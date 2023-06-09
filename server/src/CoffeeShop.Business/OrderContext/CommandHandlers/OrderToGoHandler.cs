﻿using AutoMapper;
using CoffeeShop.Core;
using CoffeeShop.Core.OrderContext.Commands;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Events;
using CoffeeShop.Domain.Repositories;
using FluentValidation;
using MediatR;
using Optional;
using Optional.Async;

namespace CoffeeShop.Business.OrderContext.CommandHandlers
{
    public class OrderToGoHandler : BaseHandler<OrderToGo>
    {
        private readonly IMenuItemsService _menuItemsService;
        private readonly IToGoOrderRepository _toGoOrderRepository;

        public OrderToGoHandler(
            IValidator<OrderToGo> validator,
            IEventBus eventBus,
            IMapper mapper,
            IMenuItemsService menuItemsService,
            IToGoOrderRepository toGoOrderRepository)
            : base(validator, eventBus, mapper)
        {
            _menuItemsService = menuItemsService;
            _toGoOrderRepository = toGoOrderRepository;
        }

        public override Task<Option<Unit, Error>> Handle(OrderToGo command) =>
            CheckIfOrderIsNotExisting(command).FlatMapAsync(order =>
            MenuItemsShouldExist(command.ItemNumbers).MapAsync(items =>
            PersistOrder(order, items)));

        private async Task<Option<ToGoOrder, Error>> CheckIfOrderIsNotExisting(OrderToGo command)
        {
            var orderInDb = await _toGoOrderRepository
                .Get(command.Id);

            return command
                .SomeWhen(_ => !orderInDb.HasValue, Error.Conflict($"Order {command.Id} already exists."))
                .Map(Mapper.Map<ToGoOrder>);
        }

        private Task<Option<IList<MenuItem>, Error>> MenuItemsShouldExist(IList<int> numbers) =>
            _menuItemsService.ItemsShouldExist(numbers);

        private Task<Unit> PersistOrder(ToGoOrder order, IList<MenuItem> orderedItems)
        {
            order.OrderedItems = orderedItems
                .Select(i => new ToGoOrderMenuItem { Id = Guid.NewGuid(), MenuItemId = i.Id, OrderId = order.Id })
                .ToList();

            return _toGoOrderRepository.Add(order);
        }
    }
}
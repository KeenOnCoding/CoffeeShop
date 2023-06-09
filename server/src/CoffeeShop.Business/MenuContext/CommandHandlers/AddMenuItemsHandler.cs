﻿using AutoMapper;
using CoffeeShop.Core.MenuContext.Commands;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Events;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;
using FluentValidation;
using MediatR;
using Optional;
using Optional.Async;

namespace CoffeeShop.Business.MenuContext.CommandHandlers
{
    public class AddMenuItemsHandler : BaseHandler<AddMenuItems>
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public AddMenuItemsHandler(
            IValidator<AddMenuItems> validator,
            IEventBus eventBus,
            IMapper mapper,
            IMenuItemRepository menuItemRepository)
            : base(validator, eventBus, mapper)
        {
            _menuItemRepository = menuItemRepository;
        }

        public override Task<Option<Unit, Error>> Handle(AddMenuItems command) =>
            CheckIfNumbersAreNotConflicting(command.MenuItems.Select(i => i.Number)).MapAsync(__ =>
            PersistMenuItems(command.MenuItems));

        private async Task<Option<Unit, Error>> CheckIfNumbersAreNotConflicting(IEnumerable<int> itemNumbersToCheck)
        {
            var numbersLookup = itemNumbersToCheck
                .Distinct()
                .ToArray();

            var conflictingNumbers = (await _menuItemRepository
                .GetAll(i => numbersLookup.Contains(i.Number)))
                .Select(i => i.Number)
                .ToArray();

            return conflictingNumbers
                .SomeWhen(
                    numbers => numbers.Length == 0,
                    Error.Conflict($"Item numbers {string.Join(", ", conflictingNumbers)} are already taken."))
                .Map(_ => Unit.Value);
        }

        private Task<Unit> PersistMenuItems(IEnumerable<MenuItemView> items)
        {
            var itemsToAdd = Mapper.Map<MenuItem[]>(items);

            return _menuItemRepository.Add(itemsToAdd);
        }
    }
}
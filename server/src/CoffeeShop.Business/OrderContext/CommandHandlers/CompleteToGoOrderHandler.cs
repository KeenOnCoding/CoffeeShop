using AutoMapper;
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
    public class CompleteToGoOrderHandler : BaseOrderHandler<CompleteToGoOrder>
    {
        private readonly IBaristaRepository _baristaRepository;

        public CompleteToGoOrderHandler(
            IValidator<CompleteToGoOrder> validator,
            IEventBus eventBus,
            IMapper mapper,
            IToGoOrderRepository toGoOrderRepository,
            IBaristaRepository baristaRepository)
            : base(validator, eventBus, mapper, toGoOrderRepository)
        {
            _baristaRepository = baristaRepository;
        }

        public override Task<Option<Unit, Error>> Handle(CompleteToGoOrder command) =>
            OrderMustExist(command.OrderId).FlatMapAsync(order =>
            OrderMustHaveStatus(ToGoOrderStatus.Issued, order).FlatMapAsync(currentStatus =>
            SetOrderStatus(ToGoOrderStatus.Completed, order).MapAsync(_ =>
            AssignOrderToBaristaIfAny(command.BaristaId, order))));

        private async Task<Unit> AssignOrderToBaristaIfAny(Option<Guid> baristaIdOption, ToGoOrder order)
        {
            await baristaIdOption.MapAsync(
                async baristaId =>
                {
                    var barista = (await _baristaRepository
                        .Get(baristaId))
                        .ValueOr(() => throw new InvalidOperationException(
                           $"Tried to assign an order to an unexisting barista. (id: {baristaId}) " +
                           $"It is very likely that the claim assigning logic is broken."));

                    barista.CompletedOrders.Add(order);
                    await _baristaRepository.Update(barista);

                    return Unit.Value;
                });

            return Unit.Value;
        }
    }
}
using AutoMapper;
using CoffeeShop.Core;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Events;
using CoffeeShop.Domain.Repositories;
using FluentValidation;
using MediatR;
using Optional;

namespace CoffeeShop.Business.OrderContext.CommandHandlers
{
    public abstract class BaseOrderHandler<TCommand> : BaseHandler<TCommand>
        where TCommand : ICommand
    {
        protected BaseOrderHandler(
            IValidator<TCommand> validator,
            IEventBus eventBus,
            IMapper mapper,
            IToGoOrderRepository toGoOrderRepository)
            : base(validator, eventBus, mapper)
        {
            ToGoOrderRepository = toGoOrderRepository;
        }

        protected IToGoOrderRepository ToGoOrderRepository { get; }

        protected Option<ToGoOrderStatus, Error> OrderMustHaveStatus(ToGoOrderStatus expectedStatus, ToGoOrder order) =>
            order.Status
                .SomeWhen(
                    status => status == expectedStatus,
                    Error.Validation($"The order must have a status of '{Enum.GetName(typeof(ToGoOrderStatus), expectedStatus)}'."));

        protected async Task<Option<ToGoOrder, Error>> OrderMustExist(Guid orderId) =>
            (await ToGoOrderRepository.Get(orderId))
                .WithException(Error.NotFound($"Order {orderId} was not found."));

        protected async Task<Option<Unit, Error>> SetOrderStatus(ToGoOrderStatus status, ToGoOrder order)
        {
            order.Status = status;

            await ToGoOrderRepository.Update(order);

            return Unit.Value.Some<Unit, Error>();
        }
    }
}
using AutoMapper;
using CoffeeShop.Core.OrderContext.Commands;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Events;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;
using FluentValidation;
using MediatR;
using Optional;
using Optional.Async;

namespace CoffeeShop.Business.OrderContext.CommandHandlers
{
    public class ConfirmToGoOrderHandler : BaseOrderHandler<ConfirmToGoOrder>
    {
        public ConfirmToGoOrderHandler(
            IValidator<ConfirmToGoOrder> validator,
            IEventBus eventBus,
            IMapper mapper,
            IToGoOrderRepository toGoOrderRepository)
            : base(validator, eventBus, mapper, toGoOrderRepository)
        {
        }

        public override Task<Option<Unit, Error>> Handle(ConfirmToGoOrder command) =>
            OrderMustExist(command.OrderId).FlatMapAsync(order =>
            OrderMustHaveStatus(ToGoOrderStatus.Pending, order).FlatMapAsync(currentStatus =>
            PaymentMustBeAtLeastWhatsOwed(order.OrderedItems.Select(i => i.MenuItem).ToList(), command.PricePaid).FlatMapAsync(totalPrice =>
            SetOrderStatus(ToGoOrderStatus.Issued, order).MapAsync(_ =>
            PublishEvents(order.Id, new OrderConfirmed { Order = Mapper.Map<ToGoOrderView>(order) })))));

        private Option<decimal, Error> PaymentMustBeAtLeastWhatsOwed(ICollection<MenuItem> orderedItems, decimal paymentAmount) =>
            orderedItems
                .Sum(i => i.Price)
                .SomeWhen(
                    total => paymentAmount >= total,
                    total => Error.Validation($"Tried to pay {paymentAmount} when the order price is {total}."));
    }
}
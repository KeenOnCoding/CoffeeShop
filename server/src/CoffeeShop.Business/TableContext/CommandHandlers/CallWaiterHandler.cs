using AutoMapper;
using CoffeeShop.Core.TableContext.Commands;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Events;
using CoffeeShop.Domain.Repositories;
using FluentValidation;
using MediatR;
using Optional;
using Optional.Async;

namespace CoffeeShop.Business.TableContext.CommandHandlers
{
    public class CallWaiterHandler : BaseTableHandler<CallWaiter>
    {
        public CallWaiterHandler(
            IValidator<CallWaiter> validator,
            IEventBus eventBus,
            IMapper mapper,
            ITableRepository tableRepository)
            : base(validator, eventBus, mapper, tableRepository)
        {
        }

        public override Task<Option<Unit, Error>> Handle(CallWaiter command) =>
            TableShouldExist(command.TableNumber).FlatMapAsync(table =>
            TableShouldHaveAWaiterAssigned(table).MapAsync(waiter =>
            PublishEvents(table.Id, new WaiterCalled { TableNumber = table.Number, WaiterId = waiter.Id })));
    }
}
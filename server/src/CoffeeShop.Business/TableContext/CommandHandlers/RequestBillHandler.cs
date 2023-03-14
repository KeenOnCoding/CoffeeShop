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
    public class RequestBillHandler : BaseTableHandler<RequestBill>
    {
        public RequestBillHandler(
            IValidator<RequestBill> validator,
            IEventBus eventBus,
            IMapper mapper,
            ITableRepository tableRepository)
            : base(validator, eventBus, mapper, tableRepository)
        {
        }

        public override Task<Option<Unit, Error>> Handle(RequestBill command) =>
            TableShouldExist(command.TableNumber).FlatMapAsync(table =>
            TableShouldHaveAWaiterAssigned(table).MapAsync(waiter =>
            PublishEvents(table.Id, new BillRequested { TableNumber = table.Number, WaiterId = waiter.Id })));
    }
}
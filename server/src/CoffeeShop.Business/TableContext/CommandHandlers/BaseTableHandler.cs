using AutoMapper;
using CoffeeShop.Core;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Events;
using CoffeeShop.Domain.Repositories;
using FluentValidation;
using Optional;

namespace CoffeeShop.Business.TableContext.CommandHandlers
{
    public abstract class BaseTableHandler<TCommand> : BaseHandler<TCommand>
        where TCommand : ICommand
    {
        protected BaseTableHandler(
            IValidator<TCommand> validator,
            IEventBus eventBus,
            IMapper mapper,
            ITableRepository tableRepository)
            : base(validator, eventBus, mapper)
        {
            TableRepository = tableRepository;
        }

        protected ITableRepository TableRepository { get; }

        protected Option<Waiter, Error> TableShouldHaveAWaiterAssigned(Table table) =>
            table
                .Waiter
                .SomeNotNull(Error.Validation($"Table {table.Number} does not have a waiter assigned."));

        protected async Task<Option<Table, Error>> TableShouldExist(int tableNumber)
        {
            var table = await TableRepository
                .GetByNumber(tableNumber);

            return table
                .WithException(Error.NotFound($"No table with number {tableNumber} was found."));
        }
    }
}
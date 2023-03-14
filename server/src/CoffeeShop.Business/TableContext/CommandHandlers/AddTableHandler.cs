using AutoMapper;
using CoffeeShop.Core.TableContext.Commands;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Events;
using CoffeeShop.Domain.Repositories;
using FluentValidation;
using MediatR;
using Optional;
using Optional.Async;

namespace CoffeeShop.Business.TableContext.CommandHandlers
{
    public class AddTableHandler : BaseTableHandler<AddTable>
    {
        public AddTableHandler(
            IValidator<AddTable> validator,
            IEventBus eventBus,
            IMapper mapper,
            ITableRepository tableRepository)
            : base(validator, eventBus, mapper, tableRepository)
        {
        }

        public override Task<Option<Unit, Error>> Handle(AddTable command) =>
            CheckIfNumberIsNotTaken(command).MapAsync(
            PersistTable);

        private Task<Unit> PersistTable(Table table) =>
            TableRepository.Add(table);

        private async Task<Option<Table, Error>> CheckIfNumberIsNotTaken(AddTable command)
        {
            var table = await TableRepository
                .GetByNumber(command.Number);

            return table
                .SomeWhen(t => !t.HasValue, Error.Conflict($"Table number '{command.Number}' is already taken."))
                .Map(_ => Mapper.Map<Table>(command));
        }
    }
}
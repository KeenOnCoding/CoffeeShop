using AutoMapper;
using CoffeeShop.Core.WaiterContext.Commands;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Events;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;
using FluentValidation;
using MediatR;
using Optional;
using Optional.Async;

namespace CoffeeShop.Business.WaiterContext.CommandHandlers
{
    public class HireWaiterHandler : BaseHandler<HireWaiter>
    {
        private readonly IWaiterRepository _waiterRepository;

        public HireWaiterHandler(
            IValidator<HireWaiter> validator,
            IEventBus eventBus,
            IMapper mapper,
            IWaiterRepository waiterRepository)
            : base(validator, eventBus, mapper)
        {
            _waiterRepository = waiterRepository;
        }

        public override Task<Option<Unit, Error>> Handle(HireWaiter command) =>
            WaiterShouldntExist(command.Id).FlatMapAsync(_ =>
            PersistWaiter(command).MapAsync(__ =>
            PublishEvents(command.Id, new WaiterHired { Waiter = Mapper.Map<WaiterView>(command) })));

        private async Task<Option<Unit, Error>> WaiterShouldntExist(Guid waiterId) =>
            (await _waiterRepository.Get(waiterId))
                .SomeWhen(w => !w.HasValue, Error.Conflict($"Waiter {waiterId} already exists."))
                .Map(_ => Unit.Value);

        private async Task<Option<Unit, Error>> PersistWaiter(HireWaiter command)
        {
            var waiter = Mapper.Map<Waiter>(command);

            await _waiterRepository.Add(waiter);

            // Returning optional so we can chain
            return Unit.Value.Some<Unit, Error>();
        }
    }
}
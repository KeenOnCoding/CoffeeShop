﻿using AutoMapper;
using CoffeeShop.Core.BaristaContext.Commands;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Events;
using CoffeeShop.Domain.Repositories;
using FluentValidation;
using MediatR;
using Optional;
using Optional.Async;

namespace CoffeeShop.Business.BaristaContext.CommandHandlers
{
    public class HireBaristaHandler : BaseHandler<HireBarista>
    {
        private readonly IBaristaRepository _baristaRepository;

        public HireBaristaHandler(
            IValidator<HireBarista> validator,
            IEventBus eventBus,
            IMapper mapper,
            IBaristaRepository baristaRepository)
            : base(validator, eventBus, mapper)
        {
            _baristaRepository = baristaRepository;
        }

        public override Task<Option<Unit, Error>> Handle(HireBarista command) =>
            BaristaShouldNotExist(command.Id).MapAsync(_ =>
            Persist(Mapper.Map<Barista>(command)));

        private Task<Unit> Persist(Barista barista) =>
            _baristaRepository.Add(barista);

        private async Task<Option<Unit, Error>> BaristaShouldNotExist(Guid id) =>
            (await _baristaRepository.Get(id))
                .SomeWhen(b => !b.HasValue, Error.Conflict($"Barista {id} already exists."))
                .Map(_ => Unit.Value);
    }
}
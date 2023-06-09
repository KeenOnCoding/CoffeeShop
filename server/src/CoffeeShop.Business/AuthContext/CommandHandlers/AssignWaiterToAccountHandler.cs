﻿using AutoMapper;
using CoffeeShop.Core.AuthContext;
using CoffeeShop.Core.AuthContext.Commands;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Events;
using CoffeeShop.Domain.Repositories;
using FluentValidation;
using MediatR;
using Optional;
using Optional.Async;

namespace CoffeeShop.Business.AuthContext.CommandHandlers
{
    public class AssignWaiterToAccountHandler : BaseAuthHandler<AssignWaiterToAccount>
    {
        private readonly IWaiterRepository _waiterRepository;

        public AssignWaiterToAccountHandler(
            IValidator<AssignWaiterToAccount> validator,
            IEventBus eventBus,
            IMapper mapper,
            IUserRepository userRepository,
            IWaiterRepository waiterRepository)
            : base(validator, eventBus, mapper, userRepository)
        {
            _waiterRepository = waiterRepository;
        }

        public override Task<Option<Unit, Error>> Handle(AssignWaiterToAccount command) =>
            AccountShouldExist(command.AccountId).FlatMapAsync(account =>
            WaiterShouldExist(command.WaiterId).MapAsync(waiter =>
            ReplaceClaim(account, AuthConstants.ClaimTypes.WaiterId, waiter.Id.ToString())));

        private Task<Option<Waiter, Error>> WaiterShouldExist(Guid waiterId) =>
            _waiterRepository
                .Get(waiterId)
                .WithException(Error.NotFound($"No waiter with id {waiterId} was found."));
    }
}
﻿using AutoMapper;
using CoffeeShop.Core;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Events;
using CoffeeShop.Domain.Repositories;
using FluentValidation;
using MediatR;
using Optional;

namespace CoffeeShop.Business.AuthContext.CommandHandlers
{
    public abstract class BaseAuthHandler<TCommand> : BaseHandler<TCommand>
        where TCommand : ICommand
    {
        protected BaseAuthHandler(
            IValidator<TCommand> validator,
            IEventBus eventBus,
            IMapper mapper,
            IUserRepository userRepository)
            : base(validator, eventBus, mapper)
        {
            UserRepository = userRepository;
        }

        protected IUserRepository UserRepository { get; }

        protected async Task<Option<User, Error>> AccountShouldExist(Guid accountId) =>
            (await UserRepository
                .Get(accountId))
                .WithException(Error.NotFound($"No account with id {accountId} was found."));

        protected Task<Unit> ReplaceClaim(User account, string claimType, string claimValue) =>
            UserRepository.ReplaceClaim(account, claimType, claimValue);
    }
}
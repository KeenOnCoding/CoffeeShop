﻿using AutoMapper;
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
    public class RegisterHandler : BaseAuthHandler<Register>
    {
        public RegisterHandler(
            IValidator<Register> validator,
            IEventBus eventBus,
            IMapper mapper,
            IUserRepository userRepository)
            : base(validator, eventBus, mapper, userRepository)
        {
        }

        public override Task<Option<Unit, Error>> Handle(Register command) =>
            CheckIfUserDoesntExist(command.Email).FlatMapAsync(_ =>
            PersistUser(command));

        private Task<Option<Unit, Error>> PersistUser(Register command)
        {
            var user = Mapper.Map<User>(command);
            return UserRepository.Register(user, command.Password);
        }

        private async Task<Option<bool, Error>> CheckIfUserDoesntExist(string email)
        {
            var user = await UserRepository.GetByEmail(email);

            return user
                .HasValue
                .SomeWhen(x => x == false, Error.Conflict($"User with email {email} already exists."));
        }
    }
}
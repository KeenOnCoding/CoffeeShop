using AutoMapper;
using CoffeeShop.Core;
using CoffeeShop.Core.TabContext.Commands;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Events;
using CoffeeShop.Domain.Repositories;
using FluentValidation;
using MediatR;
using Optional;
using Optional.Async;

namespace CoffeeShop.Business.TabContext.CommandHandlers
{
    public class CloseTabHandler : BaseTabHandler<CloseTab>
    {
        public CloseTabHandler(
            ITabRepository tabRepository,
            IMenuItemsService menuItemsService,
            IValidator<CloseTab> validator,
            IEventBus eventBus,
            IMapper mapper)
            : base(tabRepository, menuItemsService, validator, eventBus, mapper)
        {
        }

        public override Task<Option<Unit, Error>> Handle(CloseTab command) =>
            TabShouldNotBeClosed(command.TabId).
            FilterAsync(async tab => command.AmountPaid >= tab.ServedItemsValue, Error.Validation("You cannot pay less than the bill amount.")).MapAsync(tab =>
            PublishEvents(tab.Id, tab.CloseTab(command.AmountPaid)));
    }
}
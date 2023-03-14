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
    public class ServeMenuItemsHandler : BaseTabHandler<ServeMenuItems>
    {
        public ServeMenuItemsHandler(
            ITabRepository tabRepository,
            IMenuItemsService menuItemsService,
            IValidator<ServeMenuItems> validator,
            IEventBus eventBus,
            IMapper mapper)
            : base(tabRepository, menuItemsService, validator, eventBus, mapper)
        {
        }

        public override Task<Option<Unit, Error>> Handle(ServeMenuItems command) =>
            TabShouldNotBeClosed(command.TabId).FlatMapAsync(tab =>
            MenuItemsShouldExist(command.ItemNumbers).MapAsync(items =>
            PublishEvents(tab.Id, tab.ServeMenuItems(items))));
    }
}
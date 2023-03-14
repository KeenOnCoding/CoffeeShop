using CoffeeShop.Core;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories;
using Optional;
using Optional.Collections;

namespace CoffeeShop.Business
{
    public class MenuItemsService : IMenuItemsService
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemsService(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        public async Task<Option<IList<MenuItem>, Error>> ItemsShouldExist(IList<int> menuItemNumbers)
        {
            var allMenuItems = (await _menuItemRepository
                .GetAll())

                // Purposefully not using a lookup to avoid possible unique key exceptions
                // since we should never have items with conflicting menu numbers
                .ToDictionary(x => x.Number);

            var itemsToServeResult = menuItemNumbers
                .Select(menuNumber => allMenuItems
                    .GetValueOrNone(menuNumber)
                    .WithException(Error.NotFound($"Menu item with number {menuNumber} was not found.")))
                .ToList();

            var errors = itemsToServeResult
                .Exceptions()
                .SelectMany(e => e.Messages)
                .ToArray();

            return errors.Any() ?
                Option.None<IList<MenuItem>, Error>(Error.NotFound(errors)) :
                itemsToServeResult.Values().ToList().Some<IList<MenuItem>, Error>();
        }
    }
}
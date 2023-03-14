using CoffeeShop.Domain;
using CoffeeShop.Domain.Entities;
using Optional;

namespace CoffeeShop.Core
{
    public interface IMenuItemsService
    {
        Task<Option<IList<MenuItem>, Error>> ItemsShouldExist(IList<int> menuItemNumbers);
    }
}
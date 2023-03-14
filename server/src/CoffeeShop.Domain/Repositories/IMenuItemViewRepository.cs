using CoffeeShop.Domain.Views;

namespace CoffeeShop.Domain.Repositories
{
    public interface IMenuItemViewRepository
    {
        Task<IList<MenuItemView>> GetAll();
    }
}
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Core.MenuContext.Commands
{
    public class AddMenuItems : ICommand
    {
        public IList<MenuItemView> MenuItems { get; set; }
    }
}
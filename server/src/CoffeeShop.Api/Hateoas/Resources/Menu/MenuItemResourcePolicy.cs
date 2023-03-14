using CoffeeShop.Api.Controllers;
using RiskFirst.Hateoas;

namespace CoffeeShop.Api.Hateoas.Resources.Menu
{
    public class MenuItemResourcePolicy : IPolicy<MenuItemResource>
    {
        public Action<LinksPolicyBuilder<MenuItemResource>> PolicyConfiguration => policy =>
        {
            policy.RequireRoutedLink(LinkNames.Menu.AddMenuItem, nameof(MenuController.AddMenuItems), null, cond => cond.AuthorizeRoute());
        };
    }
}
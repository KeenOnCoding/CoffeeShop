using CoffeeShop.Api.Controllers;
using RiskFirst.Hateoas;

namespace CoffeeShop.Api.Hateoas.Resources.Menu
{
    public class AddMenuItemsResourcePolicy : IPolicy<AddMenuItemsResource>
    {
        public Action<LinksPolicyBuilder<AddMenuItemsResource>> PolicyConfiguration => policy =>
        {
            policy.RequireSelfLink();
            policy.RequireRoutedLink(LinkNames.Menu.GetAllMenuItems, nameof(MenuController.GetMenuItems));
        };
    }
}
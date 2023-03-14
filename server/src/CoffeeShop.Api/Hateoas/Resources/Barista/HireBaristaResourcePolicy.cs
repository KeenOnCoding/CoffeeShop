using CoffeeShop.Api.Controllers;
using RiskFirst.Hateoas;

namespace CoffeeShop.Api.Hateoas.Resources.Barista
{
    public class HireBaristaResourcePolicy : IPolicy<HireBaristaResource>
    {
        public Action<LinksPolicyBuilder<HireBaristaResource>> PolicyConfiguration => policy =>
        {
            policy.RequireSelfLink();
            policy.RequireRoutedLink(LinkNames.Barista.GetAll, nameof(BaristaController.GetEmployedBaristas));
        };
    }
}
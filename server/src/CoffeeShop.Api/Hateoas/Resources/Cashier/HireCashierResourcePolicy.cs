using CoffeeShop.Api.Controllers;
using RiskFirst.Hateoas;

namespace CoffeeShop.Api.Hateoas.Resources.Cashier
{
    public class HireCashierResourcePolicy : IPolicy<HireCashierResource>
    {
        public Action<LinksPolicyBuilder<HireCashierResource>> PolicyConfiguration => policy =>
        {
            policy.RequireSelfLink();
            policy.RequireRoutedLink(LinkNames.Cashier.GetAll, nameof(CashierController.GetEmployedCashiers));
        };
    }
}
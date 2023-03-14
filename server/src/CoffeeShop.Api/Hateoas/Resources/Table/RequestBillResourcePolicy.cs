using CoffeeShop.Api.Controllers;
using RiskFirst.Hateoas;

namespace CoffeeShop.Api.Hateoas.Resources.Table
{
    public class RequestBillResourcePolicy : IPolicy<RequestBillResource>
    {
        public Action<LinksPolicyBuilder<RequestBillResource>> PolicyConfiguration => policy =>
        {
            policy.RequireSelfLink();
            policy.RequireRoutedLink(LinkNames.Table.CallWaiter, nameof(TableController.CallWaiter), x => new { tableNumber = x.TableNumber });
            policy.RequireRoutedLink(LinkNames.Table.GetAll, nameof(TableController.GetAllTables));
        };
    }
}
using RiskFirst.Hateoas;

namespace CoffeeShop.Api.Hateoas.Resources.Tab
{
    public class TabsContainerResourcePolicy : IPolicy<TabsContainerResource>
    {
        public Action<LinksPolicyBuilder<TabsContainerResource>> PolicyConfiguration => policy =>
        {
            policy.RequireSelfLink();
        };
    }
}
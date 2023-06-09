﻿using CoffeeShop.Api.Controllers;
using RiskFirst.Hateoas;

namespace CoffeeShop.Api.Hateoas.Resources.Waiter
{
    public class HireWaiterResourcePolicy : IPolicy<HireWaiterResource>
    {
        public Action<LinksPolicyBuilder<HireWaiterResource>> PolicyConfiguration => policy =>
        {
            policy.RequireSelfLink();
            policy.RequireRoutedLink(LinkNames.Waiter.GetEmployedWaiters, nameof(WaiterController.GetEmployedWaiters));
            policy.RequireRoutedLink(LinkNames.Waiter.AssignTable, nameof(WaiterController.AssignTable));
        };
    }
}
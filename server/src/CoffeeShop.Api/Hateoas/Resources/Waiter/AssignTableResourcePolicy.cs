﻿using CoffeeShop.Api.Controllers;
using RiskFirst.Hateoas;

namespace CoffeeShop.Api.Hateoas.Resources.Waiter
{
    public class AssignTableResourcePolicy : IPolicy<AssignTableResource>
    {
        public Action<LinksPolicyBuilder<AssignTableResource>> PolicyConfiguration => policy =>
        {
            policy.RequireSelfLink();
            policy.RequireRoutedLink(LinkNames.Waiter.GetEmployedWaiters, nameof(WaiterController.GetEmployedWaiters));
            policy.RequireRoutedLink(LinkNames.Waiter.Hire, nameof(WaiterController.HireWaiter));
        };
    }
}
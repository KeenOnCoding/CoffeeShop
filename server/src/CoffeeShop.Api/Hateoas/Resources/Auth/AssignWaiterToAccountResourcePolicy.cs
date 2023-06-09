﻿using CoffeeShop.Api.Controllers;
using RiskFirst.Hateoas;

namespace CoffeeShop.Api.Hateoas.Resources.Auth
{
    public class AssignWaiterToAccountResourcePolicy : IPolicy<AssignWaiterToAccountResource>
    {
        public Action<LinksPolicyBuilder<AssignWaiterToAccountResource>> PolicyConfiguration => policy =>
        {
            policy.RequireSelfLink();
            policy.RequireRoutedLink(LinkNames.Auth.AssignBarista, nameof(AuthController.AssignBaristaToAccount));
            policy.RequireRoutedLink(LinkNames.Auth.AssignManager, nameof(AuthController.AssignManagerToAccount));
            policy.RequireRoutedLink(LinkNames.Auth.AssignCashier, nameof(AuthController.AssignCashierToAccount));
        };
    }
}
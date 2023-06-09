﻿using CoffeeShop.Api.Controllers;
using RiskFirst.Hateoas;

namespace CoffeeShop.Api.Hateoas.Resources.Manager
{
    public class HireManagerResourcePolicy : IPolicy<HireManagerResource>
    {
        public Action<LinksPolicyBuilder<HireManagerResource>> PolicyConfiguration => policy =>
        {
            policy.RequireSelfLink();
            policy.RequireRoutedLink(LinkNames.Manager.GetAll, nameof(ManagerController.GetEmployedManagers));
        };
    }
}
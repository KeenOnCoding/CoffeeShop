﻿using CoffeeShop.Api.Controllers;
using RiskFirst.Hateoas;

namespace CoffeeShop.Api.Hateoas.Resources.Table
{
    public class TableContainerResourcePolicy : IPolicy<TableContainerResource>
    {
        public Action<LinksPolicyBuilder<TableContainerResource>> PolicyConfiguration => policy =>
        {
            policy.RequireSelfLink();
            policy.RequireRoutedLink(LinkNames.Table.Add, nameof(TableController.AddTable));
        };
    }
}
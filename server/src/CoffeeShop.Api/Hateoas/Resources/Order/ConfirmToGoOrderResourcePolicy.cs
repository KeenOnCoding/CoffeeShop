﻿using CoffeeShop.Api.Controllers;
using RiskFirst.Hateoas;

namespace CoffeeShop.Api.Hateoas.Resources.Order
{
    public class ConfirmToGoOrderResourcePolicy : IPolicy<ConfirmToGoOrderResource>
    {
        public Action<LinksPolicyBuilder<ConfirmToGoOrderResource>> PolicyConfiguration => policy =>
        {
            policy.RequireSelfLink();
            policy.RequireRoutedLink(LinkNames.Order.Get, nameof(OrderController.GetOrder), x => new { id = x.OrderId });
            policy.RequireRoutedLink(LinkNames.Order.GetAll, nameof(OrderController.GetAllOrders));
        };
    }
}
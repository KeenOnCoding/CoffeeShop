﻿namespace CoffeeShop.Api.Hateoas.Resources.Cashier
{
    public class CashierResource : Resource
    {
        public Guid Id { get; set; }

        public string ShortName { get; set; }
    }
}
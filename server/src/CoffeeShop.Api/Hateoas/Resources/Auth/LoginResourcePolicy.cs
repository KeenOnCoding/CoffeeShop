﻿using CoffeeShop.Api.Controllers;
using RiskFirst.Hateoas;

namespace CoffeeShop.Api.Hateoas.Resources.Auth
{
    public class LoginResourcePolicy : IPolicy<LoginResource>
    {
        public Action<LinksPolicyBuilder<LoginResource>> PolicyConfiguration => policy =>
        {
            policy.RequireSelfLink();
            policy.RequireRoutedLink(LinkNames.Auth.GetCurrentUser, nameof(AuthController.GetCurrentUser));
            policy.RequireRoutedLink(LinkNames.Auth.Logout, nameof(AuthController.Logout));
        };
    }
}
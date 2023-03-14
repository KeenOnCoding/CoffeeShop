using CoffeeShop.Api.Controllers;
using RiskFirst.Hateoas;

namespace CoffeeShop.Api.Hateoas.Resources.Auth
{
    public class LogoutResourcePolicy : IPolicy<LogoutResource>
    {
        public Action<LinksPolicyBuilder<LogoutResource>> PolicyConfiguration => policy =>
        {
            policy.RequireRoutedLink(LinkNames.Auth.Login, nameof(AuthController.Login));
            policy.RequireRoutedLink(LinkNames.Auth.Register, nameof(AuthController.Register));
        };
    }
}
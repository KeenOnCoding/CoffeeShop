using CoffeeShop.Api.Controllers;
using RiskFirst.Hateoas;

namespace CoffeeShop.Api.Hateoas.Resources.Auth
{
    public class RegisterResourcePolicy : IPolicy<RegisterResource>
    {
        public Action<LinksPolicyBuilder<RegisterResource>> PolicyConfiguration => policy =>
        {
            policy.RequireSelfLink();
            policy.RequireRoutedLink(LinkNames.Auth.Login, nameof(AuthController.Login));
        };
    }
}
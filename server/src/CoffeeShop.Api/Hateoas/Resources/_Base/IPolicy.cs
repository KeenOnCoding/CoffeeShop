using RiskFirst.Hateoas;

namespace CoffeeShop.Api.Hateoas.Resources
{
    public interface IPolicy<TResource>
        where TResource : Resource
    {
        Action<LinksPolicyBuilder<TResource>> PolicyConfiguration { get; }
    }
}
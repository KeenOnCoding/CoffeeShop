namespace CoffeeShop.Api.Hateoas.Resources
{
    public class ResourceContainer<TResouce> : Resource
        where TResouce : Resource
    {
        public IEnumerable<TResouce> Items { get; set; }
    }
}
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Api.Hateoas.Resources.Barista
{
    public class BaristaResource : Resource
    {
        public Guid Id { get; set; }

        public string ShortName { get; set; }

        public IList<ToGoOrderView> CompletedOrders { get; set; }
    }
}
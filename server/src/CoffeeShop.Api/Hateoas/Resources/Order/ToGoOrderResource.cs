using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Api.Hateoas.Resources.Order
{
    public class ToGoOrderResource : Resource
    {
        public Guid Id { get; set; }

        public IList<MenuItemView> OrderedItems { get; set; }

        public ToGoOrderStatus Status { get; set; }

        public DateTime Date { get; set; }

        public string StatusText => Enum.GetName(typeof(ToGoOrderStatus), Status);

        public decimal Price => OrderedItems?.Sum(i => i.Price) ?? 0;
    }
}
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Domain.Events
{
    public class OrderConfirmed : IEvent
    {
        public ToGoOrderView Order { get; set; }
    }
}
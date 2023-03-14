using CoffeeShop.Domain.Views;

namespace CoffeeShop.Domain.Events
{
    public class WaiterHired : IEvent
    {
        public WaiterView Waiter { get; set; }
    }
}
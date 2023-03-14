using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Domain.Events
{
    public class MenuItemsServed : IEvent
    {
        public Guid Id { get; set; }

        public IList<MenuItem> MenuItems { get; set; }
    }
}
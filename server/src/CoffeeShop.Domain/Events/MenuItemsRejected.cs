using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Domain.Events
{
    public class MenuItemsRejected : IEvent
    {
        public Guid Id { get; set; }

        public IList<MenuItem> MenuItems { get; set; }
    }
}
namespace CoffeeShop.Domain.Entities
{
    public class ToGoOrder
    {
        public Guid Id { get; set; }

        public ICollection<ToGoOrderMenuItem> OrderedItems { get; set; }

        public ToGoOrderStatus Status { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
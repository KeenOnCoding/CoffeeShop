namespace CoffeeShop.Core.OrderContext.Commands
{
    public class OrderToGo : ICommand
    {
        public Guid Id { get; set; }

        public IList<int> ItemNumbers { get; set; }
    }
}
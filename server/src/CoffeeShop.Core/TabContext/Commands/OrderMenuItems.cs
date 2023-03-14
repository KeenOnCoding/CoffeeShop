namespace CoffeeShop.Core.TabContext.Commands
{
    public class OrderMenuItems : ICommand
    {
        public Guid TabId { get; set; }

        public IList<int> ItemNumbers { get; set; }
    }
}
namespace CoffeeShop.Core.TabContext.Commands
{
    public class ServeMenuItems : ICommand
    {
        public Guid TabId { get; set; }

        public IList<int> ItemNumbers { get; set; }
    }
}
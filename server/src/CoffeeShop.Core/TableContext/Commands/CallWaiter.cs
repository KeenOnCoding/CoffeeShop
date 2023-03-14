namespace CoffeeShop.Core.TableContext.Commands
{
    public class CallWaiter : ICommand
    {
        public int TableNumber { get; set; }
    }
}
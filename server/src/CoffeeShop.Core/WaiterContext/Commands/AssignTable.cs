namespace CoffeeShop.Core.WaiterContext.Commands
{
    public class AssignTable : ICommand
    {
        public int TableNumber { get; set; }

        public Guid WaiterId { get; set; }
    }
}
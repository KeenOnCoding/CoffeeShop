namespace CoffeeShop.Core.AuthContext.Commands
{
    public class AssignWaiterToAccount : ICommand
    {
        public Guid WaiterId { get; set; }

        public Guid AccountId { get; set; }
    }
}
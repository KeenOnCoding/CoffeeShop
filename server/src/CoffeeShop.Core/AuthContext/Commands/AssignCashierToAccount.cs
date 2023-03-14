namespace CoffeeShop.Core.AuthContext.Commands
{
    public class AssignCashierToAccount : ICommand
    {
        public Guid CashierId { get; set; }

        public Guid AccountId { get; set; }
    }
}
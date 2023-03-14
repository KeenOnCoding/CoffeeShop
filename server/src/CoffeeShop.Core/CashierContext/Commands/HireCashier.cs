namespace CoffeeShop.Core.CashierContext.Commands
{
    public class HireCashier : ICommand
    {
        public Guid Id { get; set; }

        public string ShortName { get; set; }
    }
}
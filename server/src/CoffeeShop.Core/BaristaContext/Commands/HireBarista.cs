namespace CoffeeShop.Core.BaristaContext.Commands
{
    public class HireBarista : ICommand
    {
        public Guid Id { get; set; }

        public string ShortName { get; set; }
    }
}
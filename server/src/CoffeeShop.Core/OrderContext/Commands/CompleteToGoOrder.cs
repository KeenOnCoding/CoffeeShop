using Newtonsoft.Json;
using Optional;

namespace CoffeeShop.Core.OrderContext.Commands
{
    public class CompleteToGoOrder : ICommand
    {
        public Guid OrderId { get; set; }

        [JsonIgnore]
        public Option<Guid> BaristaId { get; set; }
    }
}
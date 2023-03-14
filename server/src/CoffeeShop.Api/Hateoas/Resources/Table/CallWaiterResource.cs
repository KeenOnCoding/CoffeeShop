using Newtonsoft.Json;

namespace CoffeeShop.Api.Hateoas.Resources.Table
{
    public class CallWaiterResource : Resource
    {
        [JsonIgnore]
        public int TableNumber { get; set; }
    }
}
using Newtonsoft.Json;

namespace CoffeeShop.Api.Hateoas.Resources.Table
{
    public class RequestBillResource : Resource
    {
        [JsonIgnore]
        public int TableNumber { get; set; }
    }
}
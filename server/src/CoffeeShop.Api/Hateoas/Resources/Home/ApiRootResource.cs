using Newtonsoft.Json;

namespace CoffeeShop.Api.Hateoas.Resources.Home
{
    public class ApiRootResource : Resource
    {
        [JsonIgnore]
        public bool IsUserLoggedIn { get; set; }
    }
}
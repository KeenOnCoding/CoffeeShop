using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.Controllers
{
    public class HealthController : Controller
    {
        [HttpGet("health")]
        public IActionResult Get() =>
            Ok();
    }
}
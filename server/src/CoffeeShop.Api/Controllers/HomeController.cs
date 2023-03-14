using CoffeeShop.Api.Hateoas.Resources;
using CoffeeShop.Api.Hateoas.Resources.Home;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.Controllers
{
    [Route("")]
    public class HomeController : ApiController
    {
        public HomeController(
            IResourceMapper resourceMapper,
            IMediator mediator)
            : base(resourceMapper, mediator)
        {
        }

        /// <summary>
        /// The root of the API.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ApiHome()
        {
            var result = await ToEmptyResourceAsync<ApiRootResource>(x => x.IsUserLoggedIn = User.Identity.IsAuthenticated);

            return Ok(result);
        }
    }
}
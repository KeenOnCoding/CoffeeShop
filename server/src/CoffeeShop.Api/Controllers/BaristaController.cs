using CoffeeShop.Api.Hateoas.Resources;
using CoffeeShop.Api.Hateoas.Resources.Barista;
using CoffeeShop.Core.AuthContext;
using CoffeeShop.Core.BaristaContext.Commands;
using CoffeeShop.Core.BaristaContext.Queries;
using CoffeeShop.Domain.Views;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Optional.Async;

namespace CoffeeShop.Api.Controllers
{
    [Authorize(Policy = AuthConstants.Policies.IsAdminOrManager)]
    public class BaristaController : ApiController
    {
        public BaristaController(IResourceMapper resourceMapper, IMediator mediator)
            : base(resourceMapper, mediator)
        {
        }

        /// <summary>
        /// Hires a new barista.
        /// </summary>
        [HttpPost(Name = nameof(HireBarista))]
        public async Task<IActionResult> HireBarista([FromBody] HireBarista command) =>
            (await Mediator.Send(command)
                .MapAsync(ToEmptyResourceAsync<HireBaristaResource>))
                .Match(Ok, Error);

        /// <summary>
        /// Retrieves currently employed baristas.
        /// </summary>
        [HttpGet(Name = nameof(GetEmployedBaristas))]
        public Task<IActionResult> GetEmployedBaristas() =>
            ResourceContainerResult<BaristaView, BaristaResource, BaristaContainerResource>(new GetEmployedBaristas());
    }
}
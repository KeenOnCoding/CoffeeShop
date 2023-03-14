using CoffeeShop.Api.Hateoas.Resources;
using CoffeeShop.Api.Hateoas.Resources.Manager;
using CoffeeShop.Core.AuthContext;
using CoffeeShop.Core.ManagerContext.Commands;
using CoffeeShop.Core.ManagerContext.Queries;
using CoffeeShop.Domain.Views;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Optional.Async;

namespace CoffeeShop.Api.Controllers
{
    [Authorize(Policy = AuthConstants.Policies.IsAdmin)]
    public class ManagerController : ApiController
    {
        public ManagerController(IResourceMapper resourceMapper, IMediator mediator)
            : base(resourceMapper, mediator)
        {
        }

        /// <summary>
        /// Retrieves all currently employed managers.
        /// </summary>
        [HttpGet(Name = nameof(GetEmployedManagers))]
        public Task<IActionResult> GetEmployedManagers() =>
            ResourceContainerResult<ManagerView, ManagerResource, ManagerContainerResource>(new GetEmployedManagers());

        /// <summary>
        /// Hires a new manager.
        /// </summary>
        [HttpPost(Name = nameof(HireManager))]
        public async Task<IActionResult> HireManager([FromBody] HireManager command) =>
            (await Mediator.Send(command)
                .MapAsync(ToEmptyResourceAsync<HireManagerResource>))
                .Match(Ok, Error);
    }
}
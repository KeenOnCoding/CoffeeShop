using CoffeeShop.Api.Hateoas.Resources;
using CoffeeShop.Api.Hateoas.Resources.Menu;
using CoffeeShop.Core.AuthContext;
using CoffeeShop.Core.MenuContext.Commands;
using CoffeeShop.Core.MenuContext.Queries;
using CoffeeShop.Domain.Views;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Optional.Async;

namespace CoffeeShop.Api.Controllers
{
    public class MenuController : ApiController
    {
        public MenuController(IResourceMapper resourceMapper, IMediator mediator)
            : base(resourceMapper, mediator)
        {
        }

        /// <summary>
        /// Retrieves a list of all items currently in the menu.
        /// </summary>
        [HttpGet("items", Name = nameof(GetMenuItems))]
        public Task<IActionResult> GetMenuItems() =>
            ResourceContainerResult<MenuItemView, MenuItemResource, MenuItemsContainerResource>(new GetAllMenuItems());

        /// <summary>
        /// Adds items to the menu.
        /// </summary>
        /// <param name="command">The items to add.</param>
        [HttpPost("items", Name = nameof(AddMenuItems))]
        [Authorize(Policy = AuthConstants.Policies.IsAdminOrManager)]
        public async Task<IActionResult> AddMenuItems([FromBody] AddMenuItems command) =>
            (await Mediator.Send(command)
                .MapAsync(ToEmptyResourceAsync<AddMenuItemsResource>))
                .Match(Ok, Error);
    }
}
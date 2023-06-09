﻿using CoffeeShop.Api.Hateoas.Resources;
using CoffeeShop.Api.Hateoas.Resources.Waiter;
using CoffeeShop.Core.AuthContext;
using CoffeeShop.Core.WaiterContext.Commands;
using CoffeeShop.Core.WaiterContext.Queries;
using CoffeeShop.Domain.Views;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Optional.Async;

namespace CoffeeShop.Api.Controllers
{
    public class WaiterController : ApiController
    {
        public WaiterController(IResourceMapper resourceMapper, IMediator mediator)
            : base(resourceMapper, mediator)
        {
        }

        /// <summary>
        /// Retrieves a list of all currently employed waiters in the café.
        /// </summary>
        [HttpGet(Name = nameof(GetEmployedWaiters))]
        [Authorize(Policy = AuthConstants.Policies.IsAdminOrManager)]
        public Task<IActionResult> GetEmployedWaiters() =>
            ResourceContainerResult<WaiterView, WaiterResource, WaitersContainerResource>(new GetEmployedWaiters());

        /// <summary>
        /// Hires a waiter in the café.
        /// </summary>
        [HttpPost("hire", Name = nameof(HireWaiter))]
        [Authorize(Policy = AuthConstants.Policies.IsAdminOrManager)]
        public async Task<IActionResult> HireWaiter([FromBody] HireWaiter command) =>
            (await Mediator.Send(command)
                .MapAsync(ToEmptyResourceAsync<HireWaiterResource>))
                .Match(Ok, Error);

        /// <summary>
        /// Assigns a table to a waiter.
        /// </summary>
        [HttpPost("table/assign", Name = nameof(AssignTable))]
        [Authorize(Policy = AuthConstants.Policies.IsAdminOrManager)]
        public async Task<IActionResult> AssignTable([FromBody] AssignTable command) =>
            (await Mediator.Send(command)
                .MapAsync(ToEmptyResourceAsync<AssignTableResource>))
                .Match(Ok, Error);
    }
}
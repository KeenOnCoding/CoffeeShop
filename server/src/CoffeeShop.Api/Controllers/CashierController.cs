using CoffeeShop.Api.Hateoas.Resources;
using CoffeeShop.Api.Hateoas.Resources.Cashier;
using CoffeeShop.Core.AuthContext;
using CoffeeShop.Core.CashierContext.Commands;
using CoffeeShop.Core.CashierContext.Queries;
using CoffeeShop.Domain.Views;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Optional.Async;

namespace CoffeeShop.Api.Controllers
{
    public class CashierController : ApiController
    {
        public CashierController(IResourceMapper resourceMapper, IMediator mediator)
            : base(resourceMapper, mediator)
        {
        }

        /// <summary>
        /// Retrieves a list of all currently employed cashiers in the café.
        /// </summary>
        [HttpGet(Name = nameof(GetEmployedCashiers))]
        [Authorize(Policy = AuthConstants.Policies.IsAdminOrManager)]
        public Task<IActionResult> GetEmployedCashiers() =>
            ResourceContainerResult<CashierView, CashierResource, CashierContainerResource>(new GetEmployedCashiers());

        /// <summary>
        /// Hires a cashier in the café.
        /// </summary>
        [HttpPost(Name = nameof(HireCashier))]
        [Authorize(Policy = AuthConstants.Policies.IsAdminOrManager)]
        public async Task<IActionResult> HireCashier([FromBody] HireCashier command) =>
            (await Mediator.Send(command)
                .MapAsync(ToEmptyResourceAsync<HireCashierResource>))
                .Match(Ok, Error);
    }
}
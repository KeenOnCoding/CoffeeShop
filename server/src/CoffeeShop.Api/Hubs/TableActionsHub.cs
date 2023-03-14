using CoffeeShop.Core.AuthContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CoffeeShop.Api.Hubs
{
    [Authorize(Policy = AuthConstants.Policies.IsAdminOrWaiter)]
    public class TableActionsHub : Hub
    {
    }
}
using CoffeeShop.Core.AuthContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CoffeeShop.Api.Hubs
{
    [Authorize(Policy = AuthConstants.Policies.IsAdminOrManager)]
    public class HiredWaitersHub : Hub
    {
    }
}
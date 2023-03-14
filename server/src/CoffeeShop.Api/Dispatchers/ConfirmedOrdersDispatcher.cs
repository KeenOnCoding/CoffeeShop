using CoffeeShop.Api.Hubs;
using CoffeeShop.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CoffeeShop.Api.Dispatchers
{
    public class ConfirmedOrdersDispatcher : INotificationHandler<OrderConfirmed>
    {
        private readonly IHubContext<ConfirmedOrdersHub> _hubContext;

        public ConfirmedOrdersDispatcher(IHubContext<ConfirmedOrdersHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task Handle(OrderConfirmed notification, CancellationToken cancellationToken) =>
            _hubContext
                .Clients
                .All
                .SendAsync(nameof(OrderConfirmed), notification, cancellationToken);
    }
}
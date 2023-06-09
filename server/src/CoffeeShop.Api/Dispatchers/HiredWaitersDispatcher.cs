﻿using CoffeeShop.Api.Hubs;
using CoffeeShop.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CoffeeShop.Api.Dispatchers
{
    public class HiredWaitersDispatcher : INotificationHandler<WaiterHired>
    {
        private readonly IHubContext<HiredWaitersHub> _hubContext;

        public HiredWaitersDispatcher(IHubContext<HiredWaitersHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task Handle(WaiterHired notification, CancellationToken cancellationToken) =>
            _hubContext
                .Clients
                .All
                .SendAsync(nameof(WaiterHired), notification);
    }
}
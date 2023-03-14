using CoffeeShop.Core;
using CoffeeShop.Core.OrderContext.Queries;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;
using Optional;

namespace CoffeeShop.Business.OrderContext.QueryHandlers
{
    public class GetToGoOrderHandler : IQueryHandler<GetToGoOrder, Option<ToGoOrderView, Error>>
    {
        private readonly IToGoOrderViewRepository _toGoOrderRepository;

        public GetToGoOrderHandler(IToGoOrderViewRepository toGoOrderRepository)
        {
            _toGoOrderRepository = toGoOrderRepository;
        }

        public async Task<Option<ToGoOrderView, Error>> Handle(GetToGoOrder request, CancellationToken cancellationToken) =>
            (await _toGoOrderRepository
                .Get(request.Id))
                .WithException(Error.NotFound($"Order {request.Id} was not found."));
    }
}
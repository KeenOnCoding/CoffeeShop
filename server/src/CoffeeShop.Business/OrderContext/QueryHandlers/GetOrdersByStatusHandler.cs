using CoffeeShop.Core;
using CoffeeShop.Core.OrderContext.Queries;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Business.OrderContext.QueryHandlers
{
    public class GetOrdersByStatusHandler : IQueryHandler<GetOrdersByStatus, IList<ToGoOrderView>>
    {
        private readonly IToGoOrderViewRepository _toGoOrderViewRepository;

        public GetOrdersByStatusHandler(IToGoOrderViewRepository toGoOrderViewRepository)
        {
            _toGoOrderViewRepository = toGoOrderViewRepository;
        }

        public Task<IList<ToGoOrderView>> Handle(GetOrdersByStatus query, CancellationToken cancellationToken) =>
            _toGoOrderViewRepository
                .GetByStatus(query.Status);
    }
}
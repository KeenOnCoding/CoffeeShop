using CoffeeShop.Core;
using CoffeeShop.Core.OrderContext.Queries;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Business.OrderContext.QueryHandlers
{
    public class GetAllToGoOrdersHandler : IQueryHandler<GetAllToGoOrders, IList<ToGoOrderView>>
    {
        private readonly IToGoOrderViewRepository _toGoOrderViewRepository;

        public GetAllToGoOrdersHandler(IToGoOrderViewRepository toGoOrderViewRepository)
        {
            _toGoOrderViewRepository = toGoOrderViewRepository;
        }

        public Task<IList<ToGoOrderView>> Handle(GetAllToGoOrders request, CancellationToken cancellationToken) =>
            _toGoOrderViewRepository
                .GetAll();
    }
}
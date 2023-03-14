using CoffeeShop.Core;
using CoffeeShop.Core.WaiterContext.Queries;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Business.WaiterContext.QueryHandlers
{
    public class GetEmployedWaitersHandler : IQueryHandler<GetEmployedWaiters, IList<WaiterView>>
    {
        private readonly IWaiterViewRepository _waiterViewRepository;

        public GetEmployedWaitersHandler(IWaiterViewRepository waiterViewRepository)
        {
            _waiterViewRepository = waiterViewRepository;
        }

        public Task<IList<WaiterView>> Handle(GetEmployedWaiters request, CancellationToken cancellationToken) =>
            _waiterViewRepository
                .GetAll();
    }
}
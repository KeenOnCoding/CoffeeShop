using CoffeeShop.Core;
using CoffeeShop.Core.BaristaContext.Queries;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Business.BaristaContext.QueryHandlers
{
    public class GetEmployedBaristasHandler : IQueryHandler<GetEmployedBaristas, IList<BaristaView>>
    {
        private readonly IBaristaViewRepository _baristaViewRepository;

        public GetEmployedBaristasHandler(IBaristaViewRepository baristaViewRepository)
        {
            _baristaViewRepository = baristaViewRepository;
        }

        public Task<IList<BaristaView>> Handle(GetEmployedBaristas request, CancellationToken cancellationToken) =>
            _baristaViewRepository
                .GetAll();
    }
}
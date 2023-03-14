using CoffeeShop.Core;
using CoffeeShop.Core.CashierContext.Queries;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Business.CashierContext.QueryHandlers
{
    public class GetEmployedCashiersHandler : IQueryHandler<GetEmployedCashiers, IList<CashierView>>
    {
        private readonly ICashierViewRepository _cashierViewRepository;

        public GetEmployedCashiersHandler(ICashierViewRepository cashierViewRepository)
        {
            _cashierViewRepository = cashierViewRepository;
        }

        public Task<IList<CashierView>> Handle(GetEmployedCashiers request, CancellationToken cancellationToken) =>
            _cashierViewRepository
                .GetAll();
    }
}
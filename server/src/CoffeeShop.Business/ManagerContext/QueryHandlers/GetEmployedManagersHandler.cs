using CoffeeShop.Core;
using CoffeeShop.Core.ManagerContext.Queries;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Business.ManagerContext.QueryHandlers
{
    public class GetEmployedManagersHandler : IQueryHandler<GetEmployedManagers, IList<ManagerView>>
    {
        private readonly IManagerViewRepository _managerViewRepository;

        public GetEmployedManagersHandler(IManagerViewRepository managerViewRepository)
        {
            _managerViewRepository = managerViewRepository;
        }

        public Task<IList<ManagerView>> Handle(GetEmployedManagers request, CancellationToken cancellationToken) =>
            _managerViewRepository
                .GetAll();
    }
}
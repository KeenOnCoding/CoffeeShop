using CoffeeShop.Core;
using CoffeeShop.Core.TabContext.Queries;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Business.TabContext.QueryHandlers
{
    public class GetAllOpenTabsHandler : IQueryHandler<GetAllOpenTabs, IList<TabView>>
    {
        private readonly ITabViewRepository _tabViewRepository;

        public GetAllOpenTabsHandler(ITabViewRepository tabViewRepository)
        {
            _tabViewRepository = tabViewRepository;
        }

        public Task<IList<TabView>> Handle(GetAllOpenTabs request, CancellationToken cancellationToken) =>
            _tabViewRepository
                .GetTabs(t => t.IsOpen);
    }
}
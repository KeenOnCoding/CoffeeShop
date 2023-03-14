using CoffeeShop.Core;
using CoffeeShop.Core.TabContext.Queries;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Business.TabContext.QueryHandlers
{
    public class GetTabHistoryHandler : IQueryHandler<GetTabHistory, IList<TabView>>
    {
        private readonly ITabViewRepository _tabViewRepository;

        public GetTabHistoryHandler(ITabViewRepository tabViewRepository)
        {
            _tabViewRepository = tabViewRepository;
        }

        public Task<IList<TabView>> Handle(GetTabHistory request, CancellationToken cancellationToken) =>
            _tabViewRepository
                .GetTabs(t => !t.IsOpen);
    }
}
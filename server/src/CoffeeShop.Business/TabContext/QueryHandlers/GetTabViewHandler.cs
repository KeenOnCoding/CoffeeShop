using CoffeeShop.Core;
using CoffeeShop.Core.TabContext.Queries;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;
using Optional;

namespace CoffeeShop.Business.TabContext.QueryHandlers
{
    public class GetTabViewHandler : IQueryHandler<GetTabView, Option<TabView, Error>>
    {
        private readonly ITabViewRepository _tabViewRepository;

        public GetTabViewHandler(ITabViewRepository tabViewRepository)
        {
            _tabViewRepository = tabViewRepository;
        }

        public async Task<Option<TabView, Error>> Handle(GetTabView request, CancellationToken cancellationToken) =>
            (await _tabViewRepository
                .Get(request.Id))
                .WithException(Error.NotFound($"No tab with an id of {request.Id} was found."));
    }
}
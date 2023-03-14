using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;
using Marten;
using Optional;
using System.Linq.Expressions;

namespace CoffeeShop.Persistance.Repositories
{
    public class TabViewRepository : ITabViewRepository
    {
        private readonly IDocumentSession _session;

        public TabViewRepository(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<Option<TabView>> Get(Guid id)
        {
            var tab = await _session
                .Query<TabView>()
                .SingleOrDefaultAsync(t => t.Id == id);

            return tab
                .SomeNotNull();
        }

        public async Task<IList<TabView>> GetTabs(Expression<Func<TabView, bool>> predicate = null) =>
            (IList<TabView>)await _session
                .Query<TabView>()
                .Where(predicate ?? (_ => true))
                .ToListAsync();
    }
}
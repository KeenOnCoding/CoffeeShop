using CoffeeShop.Domain.Views;
using Optional;
using System.Linq.Expressions;

namespace CoffeeShop.Domain.Repositories
{
    public interface ITabViewRepository
    {
        Task<Option<TabView>> Get(Guid id);

        Task<IList<TabView>> GetTabs(Expression<Func<TabView, bool>> predicate = null);
    }
}
using CoffeeShop.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace CoffeeShop.Domain.Repositories
{
    public interface IMenuItemRepository
    {
        Task<Unit> Add(params MenuItem[] item);

        Task<IList<MenuItem>> GetAll(Expression<Func<MenuItem, bool>> predicate = null);
    }
}
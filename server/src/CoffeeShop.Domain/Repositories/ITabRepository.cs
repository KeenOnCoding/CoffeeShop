using CoffeeShop.Domain.Entities;
using Optional;

namespace CoffeeShop.Domain.Repositories
{
    public interface ITabRepository
    {
        Task<Option<Tab>> Get(Guid id);
    }
}
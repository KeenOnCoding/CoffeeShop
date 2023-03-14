using CoffeeShop.Domain.Entities;
using MediatR;
using Optional;

namespace CoffeeShop.Domain.Repositories
{
    public interface IManagerRepository
    {
        Task<Option<Manager>> Get(Guid id);

        Task<Unit> Add(Manager manager);
    }
}
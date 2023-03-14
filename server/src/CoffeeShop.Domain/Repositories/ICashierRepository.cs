using CoffeeShop.Domain.Entities;
using MediatR;
using Optional;

namespace CoffeeShop.Domain.Repositories
{
    public interface ICashierRepository
    {
        Task<Option<Cashier>> Get(Guid id);

        Task<Unit> Add(Cashier cashier);
    }
}
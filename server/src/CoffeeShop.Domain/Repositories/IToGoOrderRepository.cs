using CoffeeShop.Domain.Entities;
using MediatR;
using Optional;

namespace CoffeeShop.Domain.Repositories
{
    public interface IToGoOrderRepository
    {
        Task<Option<ToGoOrder>> Get(Guid id);

        Task<Unit> Add(ToGoOrder order);

        Task<Unit> Update(ToGoOrder order);
    }
}
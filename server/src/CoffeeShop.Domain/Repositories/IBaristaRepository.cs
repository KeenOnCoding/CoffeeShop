using CoffeeShop.Domain.Entities;
using MediatR;
using Optional;

namespace CoffeeShop.Domain.Repositories
{
    public interface IBaristaRepository
    {
        Task<Option<Barista>> Get(Guid id);

        Task<Unit> Update(Barista barista);

        Task<Unit> Add(Barista barista);
    }
}
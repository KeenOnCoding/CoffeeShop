using CoffeeShop.Domain.Entities;
using MediatR;
using Optional;

namespace CoffeeShop.Domain.Repositories
{
    public interface ITableRepository
    {
        Task<Option<Table>> GetByNumber(int tableNumber);

        Task<Unit> Add(Table table);

        Task<Unit> Update(Table table);
    }
}
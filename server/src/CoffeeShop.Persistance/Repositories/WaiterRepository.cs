using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Persistance.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace CoffeeShop.Persistance.Repositories
{
    public class WaiterRepository : IWaiterRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public WaiterRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Add(Waiter waiter)
        {
            await _dbContext.AddAsync(waiter);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        public async Task<Option<Waiter>> Get(Guid id) =>
            (await _dbContext
                .Waiters
                .Include(w => w.ServedTables)
                .FirstOrDefaultAsync(w => w.Id == id))
            .SomeNotNull();
    }
}
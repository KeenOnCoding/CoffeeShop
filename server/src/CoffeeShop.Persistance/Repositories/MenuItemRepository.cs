using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Persistance.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CoffeeShop.Persistance.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MenuItemRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Add(params MenuItem[] items)
        {
            _dbContext.MenuItems.AddRange(items);
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<IList<MenuItem>> GetAll(Expression<Func<MenuItem, bool>> predicate = null) =>
            await _dbContext
                .MenuItems
                .Where(predicate ?? (_ => true))
                .ToListAsync();
    }
}
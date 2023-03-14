using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;
using CoffeeShop.Persistance.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Persistance.Repositories
{
    public class MenuItemViewRepository : IMenuItemViewRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public MenuItemViewRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IList<MenuItemView>> GetAll() =>
            await _dbContext
                .MenuItems
                .ProjectTo<MenuItemView>(_mapper.ConfigurationProvider)
                .ToListAsync();
    }
}
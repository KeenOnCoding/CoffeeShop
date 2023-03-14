using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;
using CoffeeShop.Persistance.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Persistance.Repositories
{
    public class WaiterViewRepository : IWaiterViewRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public WaiterViewRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IList<WaiterView>> GetAll() =>
            await _dbContext
                .Waiters
                .ProjectTo<WaiterView>(_mapper.ConfigurationProvider)
                .ToListAsync();
    }
}
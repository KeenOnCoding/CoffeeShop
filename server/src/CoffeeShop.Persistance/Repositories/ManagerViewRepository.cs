using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;
using CoffeeShop.Persistance.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Persistance.Repositories
{
    public class ManagerViewRepository : IManagerViewRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ManagerViewRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IList<ManagerView>> GetAll() =>
            await _dbContext
                .Managers
                .ProjectTo<ManagerView>(_mapper.ConfigurationProvider)
                .ToListAsync();
    }
}
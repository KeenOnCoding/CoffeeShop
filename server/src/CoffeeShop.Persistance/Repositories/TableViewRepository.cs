using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;
using CoffeeShop.Persistance.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Persistance.Repositories
{
    public class TableViewRepository : ITableViewRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public TableViewRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IList<TableView>> GetAll() =>
            await _dbContext
                .Tables
                .ProjectTo<TableView>(_mapper.ConfigurationProvider)
                .ToListAsync();
    }
}
﻿using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Persistance.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Optional;
using Optional.Async;

namespace CoffeeShop.Persistance.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TableRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Add(Table table)
        {
            _dbContext.Add(table);
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }

        public Task<Option<Table>> GetByNumber(int tableNumber) =>
            _dbContext
                .Tables
                .Include(t => t.Waiter)
                .FirstOrDefaultAsync(t => t.Number == tableNumber)
                .SomeNotNull();

        public async Task<Unit> Update(Table table)
        {
            _dbContext.Tables.Update(table);
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
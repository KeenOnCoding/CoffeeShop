﻿using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Persistance.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace CoffeeShop.Persistance.Repositories
{
    public class BaristaRepository : IBaristaRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BaristaRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Add(Barista barista)
        {
            _dbContext.Baristas.Add(barista);
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<Option<Barista>> Get(Guid id) =>
            (await _dbContext
                .Baristas
                .Include(b => b.CompletedOrders)
                .FirstOrDefaultAsync(b => b.Id == id))
                .SomeNotNull();

        public async Task<Unit> Update(Barista barista)
        {
            _dbContext.Baristas.Update(barista);
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
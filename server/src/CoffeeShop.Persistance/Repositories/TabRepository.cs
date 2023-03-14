using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories;
using Marten;
using Optional;
using Optional.Async;

namespace CoffeeShop.Persistance.Repositories
{
    public class TabRepository : ITabRepository
    {
        private readonly IDocumentSession _session;

        public TabRepository(IDocumentSession session)
        {
            _session = session;
        }

        public Task<Option<Tab>> Get(Guid id) =>
            _session
                .LoadAsync<Tab>(id)
                .SomeNotNull();
    }
}
using CoffeeShop.Domain;
using CoffeeShop.Domain.Views;
using Optional;

namespace CoffeeShop.Core.AuthContext.Queries
{
    public class GetUser : IQuery<Option<UserView, Error>>
    {
        public Guid Id { get; set; }
    }
}
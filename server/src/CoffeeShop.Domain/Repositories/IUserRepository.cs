using CoffeeShop.Domain.Entities;
using MediatR;
using Optional;
using System.Security.Claims;

namespace CoffeeShop.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<Option<User>> Get(Guid id);

        Task<Option<User>> GetByEmail(string email);

        Task<Unit> ReplaceClaim(User account, string claimType, string claimValue);

        Task<Option<Unit, Error>> Register(User user, string password);

        Task<bool> CheckPassword(User user, string password);

        Task<IList<Claim>> GetClaims(User user);
    }
}
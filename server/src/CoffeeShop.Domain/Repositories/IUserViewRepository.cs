using CoffeeShop.Domain.Views;
using Optional;

namespace CoffeeShop.Domain.Repositories
{
    public interface IUserViewRepository
    {
        Task<IList<UserView>> GetAll();

        Task<Option<UserView>> Get(Guid id);
    }
}
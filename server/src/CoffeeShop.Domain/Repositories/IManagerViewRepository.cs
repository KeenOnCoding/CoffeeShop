using CoffeeShop.Domain.Views;

namespace CoffeeShop.Domain.Repositories
{
    public interface IManagerViewRepository
    {
        Task<IList<ManagerView>> GetAll();
    }
}
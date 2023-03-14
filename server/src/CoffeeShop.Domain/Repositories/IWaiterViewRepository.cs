using CoffeeShop.Domain.Views;

namespace CoffeeShop.Domain.Repositories
{
    public interface IWaiterViewRepository
    {
        Task<IList<WaiterView>> GetAll();
    }
}
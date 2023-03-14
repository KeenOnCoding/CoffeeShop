using CoffeeShop.Domain.Views;

namespace CoffeeShop.Domain.Repositories
{
    public interface IBaristaViewRepository
    {
        Task<IList<BaristaView>> GetAll();
    }
}
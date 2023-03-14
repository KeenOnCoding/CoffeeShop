using CoffeeShop.Domain.Views;

namespace CoffeeShop.Domain.Repositories
{
    public interface ICashierViewRepository
    {
        Task<IList<CashierView>> GetAll();
    }
}
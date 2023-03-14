using CoffeeShop.Domain.Views;

namespace CoffeeShop.Domain.Repositories
{
    public interface ITableViewRepository
    {
        Task<IList<TableView>> GetAll();
    }
}
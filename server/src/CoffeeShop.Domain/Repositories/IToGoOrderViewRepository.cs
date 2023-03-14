using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Views;
using Optional;

namespace CoffeeShop.Domain.Repositories
{
    public interface IToGoOrderViewRepository
    {
        Task<Option<ToGoOrderView>> Get(Guid id);

        Task<IList<ToGoOrderView>> GetByStatus(ToGoOrderStatus status);

        Task<IList<ToGoOrderView>> GetAll();
    }
}
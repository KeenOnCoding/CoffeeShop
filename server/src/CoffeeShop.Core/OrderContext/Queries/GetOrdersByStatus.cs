using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Core.OrderContext.Queries
{
    public class GetOrdersByStatus : IQuery<IList<ToGoOrderView>>
    {
        public ToGoOrderStatus Status { get; set; }
    }
}
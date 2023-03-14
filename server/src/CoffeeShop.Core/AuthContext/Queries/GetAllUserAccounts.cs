using CoffeeShop.Domain.Views;

namespace CoffeeShop.Core.AuthContext.Queries
{
    public class GetAllUserAccounts : IQuery<IList<UserView>>
    {
    }
}
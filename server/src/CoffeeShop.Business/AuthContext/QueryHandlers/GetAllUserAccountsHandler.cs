using CoffeeShop.Core;
using CoffeeShop.Core.AuthContext.Queries;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Business.AuthContext.QueryHandlers
{
    public class GetAllUserAccountsHandler : IQueryHandler<GetAllUserAccounts, IList<UserView>>
    {
        private readonly IUserViewRepository _userViewRepository;

        public GetAllUserAccountsHandler(IUserViewRepository userViewRepository)
        {
            _userViewRepository = userViewRepository;
        }

        public Task<IList<UserView>> Handle(GetAllUserAccounts request, CancellationToken cancellationToken) =>
            _userViewRepository
                .GetAll();
    }
}
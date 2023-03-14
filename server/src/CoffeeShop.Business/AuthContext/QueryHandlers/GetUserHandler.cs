using CoffeeShop.Core;
using CoffeeShop.Core.AuthContext.Queries;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;
using Optional;

namespace CoffeeShop.Business.AuthContext.QueryHandlers
{
    public class GetUserHandler : IQueryHandler<GetUser, Option<UserView, Error>>
    {
        private readonly IUserViewRepository _userViewRepository;

        public GetUserHandler(IUserViewRepository userViewRepository)
        {
            _userViewRepository = userViewRepository;
        }

        public async Task<Option<UserView, Error>> Handle(GetUser request, CancellationToken cancellationToken) =>
            (await _userViewRepository.Get(request.Id))
                .WithException(Error.NotFound($"No user with id {request.Id} was found."));
    }
}
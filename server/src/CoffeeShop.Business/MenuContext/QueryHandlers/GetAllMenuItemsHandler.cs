using CoffeeShop.Core;
using CoffeeShop.Core.MenuContext.Queries;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Business.MenuContext.QueryHandlers
{
    public class GetAllMenuItemsHandler : IQueryHandler<GetAllMenuItems, IList<MenuItemView>>
    {
        private readonly IMenuItemViewRepository _menuItemViewRepository;

        public GetAllMenuItemsHandler(IMenuItemViewRepository menuItemViewRepository)
        {
            _menuItemViewRepository = menuItemViewRepository;
        }

        public Task<IList<MenuItemView>> Handle(GetAllMenuItems request, CancellationToken cancellationToken) =>
            _menuItemViewRepository
                .GetAll();
    }
}
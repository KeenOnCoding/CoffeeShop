using AutoMapper;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Api.Hateoas.Resources.Menu
{
    public class MenuMappingProfile : Profile
    {
        public MenuMappingProfile()
        {
            CreateMap<MenuItemView, MenuItemResource>(MemberList.Destination);
        }
    }
}
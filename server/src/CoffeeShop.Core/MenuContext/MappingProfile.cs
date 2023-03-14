using AutoMapper;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Core.MenuContext
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MenuItemView, MenuItem>(MemberList.Source);
            CreateMap<MenuItem, MenuItemView>(MemberList.Destination);
        }
    }
}
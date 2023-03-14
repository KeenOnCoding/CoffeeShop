using AutoMapper;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Api.Hateoas.Resources.Tab
{
    public class TabMappingProfile : Profile
    {
        public TabMappingProfile()
        {
            CreateMap<TabView, TabResource>(MemberList.Destination);
        }
    }
}
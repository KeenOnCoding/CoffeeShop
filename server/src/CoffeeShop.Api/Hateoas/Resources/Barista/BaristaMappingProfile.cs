using AutoMapper;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Api.Hateoas.Resources.Barista
{
    public class BaristaMappingProfile : Profile
    {
        public BaristaMappingProfile()
        {
            CreateMap<BaristaView, BaristaResource>(MemberList.Destination);
        }
    }
}
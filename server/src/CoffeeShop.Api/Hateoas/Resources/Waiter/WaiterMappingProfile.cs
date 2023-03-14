using AutoMapper;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Api.Hateoas.Resources.Waiter
{
    public class WaiterMappingProfile : Profile
    {
        public WaiterMappingProfile()
        {
            CreateMap<WaiterView, WaiterResource>(MemberList.Destination);
        }
    }
}
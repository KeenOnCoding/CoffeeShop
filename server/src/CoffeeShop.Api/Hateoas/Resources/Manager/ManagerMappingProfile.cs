using AutoMapper;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Api.Hateoas.Resources.Manager
{
    public class ManagerMappingProfile : Profile
    {
        public ManagerMappingProfile()
        {
            CreateMap<ManagerView, ManagerResource>(MemberList.Destination);
        }
    }
}
using AutoMapper;
using CoffeeShop.Core.ManagerContext.Commands;
using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Core.ManagerContext
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HireManager, Manager>(MemberList.Source);
        }
    }
}
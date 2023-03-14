using AutoMapper;
using CoffeeShop.Core.BaristaContext.Commands;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Core.BaristaContext
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HireBarista, Barista>(MemberList.Source);

            CreateMap<Barista, BaristaView>(MemberList.Destination);
        }
    }
}
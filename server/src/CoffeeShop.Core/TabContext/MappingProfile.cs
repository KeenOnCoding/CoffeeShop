using AutoMapper;
using CoffeeShop.Core.TableContext.Commands;
using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Core.TabContext
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddTable, Table>(MemberList.Source);
        }
    }
}
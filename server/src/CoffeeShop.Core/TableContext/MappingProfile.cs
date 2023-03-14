using AutoMapper;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Core.TableContext
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Table, TableView>(MemberList.Destination);
        }
    }
}
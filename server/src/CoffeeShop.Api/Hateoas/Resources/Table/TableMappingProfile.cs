using AutoMapper;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Api.Hateoas.Resources.Table
{
    public class TableMappingProfile : Profile
    {
        public TableMappingProfile()
        {
            CreateMap<TableView, TableResource>(MemberList.Destination);
        }
    }
}
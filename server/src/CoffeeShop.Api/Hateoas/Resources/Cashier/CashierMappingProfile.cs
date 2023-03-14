using AutoMapper;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Api.Hateoas.Resources.Cashier
{
    public class CashierMappingProfile : Profile
    {
        public CashierMappingProfile()
        {
            CreateMap<CashierView, CashierResource>(MemberList.Destination);
        }
    }
}
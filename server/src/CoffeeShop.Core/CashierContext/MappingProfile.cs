using AutoMapper;
using CoffeeShop.Core.CashierContext.Commands;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Core.CashierContext
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HireCashier, Cashier>(MemberList.Source);
            CreateMap<Cashier, CashierView>(MemberList.Destination);
        }
    }
}
using AutoMapper;
using CoffeeShop.Core.WaiterContext.Commands;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Core.WaiterContext
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HireWaiter, Waiter>(MemberList.Source);
            CreateMap<HireWaiter, WaiterView>(MemberList.Source);
            CreateMap<Waiter, WaiterView>(MemberList.Destination)
                .ForMember(d => d.TablesServed, opts => opts.MapFrom(s => s.ServedTables.Select(t => t.Number)));
        }
    }
}
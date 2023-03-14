using AutoMapper;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Api.Hateoas.Resources.Auth
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<JwtView, LoginResource>(MemberList.Destination);
            CreateMap<UserView, UserResource>(MemberList.Destination);
        }
    }
}
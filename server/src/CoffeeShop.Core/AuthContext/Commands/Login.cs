using CoffeeShop.Domain.Views;

namespace CoffeeShop.Core.AuthContext.Commands
{
    public class Login : ICommand<JwtView>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
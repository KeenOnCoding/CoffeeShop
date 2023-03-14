using FluentValidation;

namespace CoffeeShop.Core.TabContext.Commands
{
    public class OpenTabValidator : AbstractValidator<OpenTab>
    {
        public OpenTabValidator()
        {
            RuleFor(c => c.Id).NotEqual(Guid.Empty);
            RuleFor(c => c.CustomerName).NotNull();
            RuleFor(c => c.CustomerName).NotEmpty();
        }
    }
}
using FluentValidation;

namespace CoffeeShop.Core.WaiterContext.Commands
{
    public class HireWaiterValidator : AbstractValidator<HireWaiter>
    {
        public HireWaiterValidator()
        {
            RuleFor(c => c.ShortName).NotNull();
            RuleFor(c => c.ShortName).NotEmpty();
        }
    }
}
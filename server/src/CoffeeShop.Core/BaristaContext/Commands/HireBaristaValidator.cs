using FluentValidation;

namespace CoffeeShop.Core.BaristaContext.Commands
{
    public class HireBaristaValidator : AbstractValidator<HireBarista>
    {
        public HireBaristaValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.ShortName).NotEmpty();
        }
    }
}
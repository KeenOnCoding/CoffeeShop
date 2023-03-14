using FluentValidation;

namespace CoffeeShop.Core.TabContext.Commands
{
    public class CloseTabValidator : AbstractValidator<CloseTab>
    {
        public CloseTabValidator()
        {
            RuleFor(c => c.TabId).NotEmpty();
            RuleFor(c => c.AmountPaid).GreaterThanOrEqualTo(0);
        }
    }
}
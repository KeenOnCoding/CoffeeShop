using FluentValidation;

namespace CoffeeShop.Core.OrderContext.Commands
{
    public class CompleteToGoOrderValidator : AbstractValidator<CompleteToGoOrder>
    {
        public CompleteToGoOrderValidator()
        {
            RuleFor(c => c.OrderId).NotEmpty();
        }
    }
}
using FluentValidation;

namespace CoffeeShop.Core.TabContext.Commands
{
    public class OrderMenuItemsValidator : AbstractValidator<OrderMenuItems>
    {
        public OrderMenuItemsValidator()
        {
            RuleFor(c => c.TabId).NotEmpty();
            RuleFor(c => c.ItemNumbers).NotEmpty();
        }
    }
}
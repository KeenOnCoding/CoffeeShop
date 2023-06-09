﻿using FluentValidation;

namespace CoffeeShop.Core.TableContext.Commands
{
    public class AddTableValidator : AbstractValidator<AddTable>
    {
        public AddTableValidator()
        {
            RuleFor(c => c.Number).GreaterThan(0);
        }
    }
}
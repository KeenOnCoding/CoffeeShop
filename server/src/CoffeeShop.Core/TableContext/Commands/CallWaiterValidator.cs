﻿using FluentValidation;

namespace CoffeeShop.Core.TableContext.Commands
{
    public class CallWaiterValidator : AbstractValidator<CallWaiter>
    {
        public CallWaiterValidator()
        {
            RuleFor(c => c.TableNumber).GreaterThan(0);
        }
    }
}
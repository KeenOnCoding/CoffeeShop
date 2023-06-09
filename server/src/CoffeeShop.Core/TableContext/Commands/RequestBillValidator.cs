﻿using FluentValidation;

namespace CoffeeShop.Core.TableContext.Commands
{
    public class RequestBillValidator : AbstractValidator<RequestBill>
    {
        public RequestBillValidator()
        {
            RuleFor(c => c.TableNumber).GreaterThan(0);
        }
    }
}
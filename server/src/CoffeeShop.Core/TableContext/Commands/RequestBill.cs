﻿namespace CoffeeShop.Core.TableContext.Commands
{
    public class RequestBill : ICommand
    {
        public int TableNumber { get; set; }
    }
}
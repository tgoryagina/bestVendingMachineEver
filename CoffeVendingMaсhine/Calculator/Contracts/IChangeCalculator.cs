using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoffeVendingMaсhine.Model;

namespace CoffeVendingMaсhine.Calculator.Contracts
{
    public interface IChangeCalculator
    {
        Wallet FindChange(Wallet wallet, int summ);
    }
}

using System.Collections.Generic;
using System.Linq;
using CoffeVendingMaсhine.Calculator.Contracts;
using CoffeVendingMaсhine.Model;

namespace CoffeVendingMaсhine.Calculator
{
    public class ChangeCalculator : IChangeCalculator
    {
        /// <summary>
        /// Метод поиска суммы наименьшим числом монет из указанного кошелька
        /// </summary>
        /// <param name="wallet"></param>
        /// <param name="summ"></param>
        /// <returns></returns>
        public Wallet FindChange(Wallet wallet, int summ)
        {
            if (wallet == null || summ <= 0)
                return null;

            List<CoinSet> coins = wallet.Coins.OrderByDescending(coin => coin.Value).ToList();

            int i = 0;

            var change = new Wallet();

            while (summ > 0 && i < coins.Count())
            {
                if (coins[i].Count > 0 && coins[i].Value <= summ)
                {
                    change.AddCoins(coins[i].Value, 1);
                    summ -= coins[i].Value;
                    coins[i].Count--;
                }
                else
                    i++;
            }

            if (summ != 0)
                change.Clear();

            return change;
        }

    }
}

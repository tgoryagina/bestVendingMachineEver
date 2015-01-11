using System;
using System.Collections.ObjectModel;
using System.Linq;
using Rikrop.Core.Wpf;

namespace CoffeVendingMaсhine.Model
{
    /// <summary>
    ///     Кошелек, умеющий оперировать с набором монет
    /// </summary>
    public class Wallet : ChangeNotifier
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public Wallet()
        {
            Coins = new ObservableCollection<CoinSet>();
        }

        /// <summary>
        ///     Набор монет в кошельке
        /// </summary>
        public ObservableCollection<CoinSet> Coins { get; private set; }

        /// <summary>
        /// Добавление кошелька к кошельку
        /// </summary>
        /// <param name="addedWallet"></param>
        public void AddWallet(Wallet addedWallet)
        {
            if (addedWallet == null)
                return;

            foreach (var coinSet in addedWallet.Coins)
                AddCoins(coinSet);
        }

        /// <summary>
        /// Удаление кошелька из кошелька
        /// </summary>
        /// <param name="removedWallet"></param>
        public void RemoveWallet(Wallet removedWallet)
        {
            foreach (CoinSet coinSet in removedWallet.Coins)
                RemoveCoins(coinSet);
        }

        /// <summary>
        ///     Добавление монет в набор
        /// </summary>
        /// <param name="value"></param>
        /// <param name="count"></param>
        public void AddCoins(int value, int count)
        {
            CoinSet coin = Coins.Where(c => c.Value == value).FirstOrDefault();

            if (coin == null)
                Coins.Add(new CoinSet(value, count));
            else
                coin.Count += count;

            NotifyPropertyChanged("Total");
        }

        public void AddCoins(CoinSet coinSet)
        {
            AddCoins(coinSet.Value, coinSet.Count);
        }

        /// <summary>
        ///     Удаление монет из набора
        /// </summary>
        /// <param name="value"></param>
        /// <param name="count"></param>
        public void RemoveCoins(int value, int count)
        {
            CoinSet coin = Coins.Where(c => c.Value == value).FirstOrDefault();

            if (coin == null)
                throw new ArgumentException(string.Format("Монет наминала {0} в наборе нет", value)); //TODO: ы?

            coin.Count -= count;
            NotifyPropertyChanged("Total");
        }

        public void RemoveCoins(CoinSet coin)
        {
            RemoveCoins(coin.Value, coin.Count);
        }

        /// <summary>
        ///     Проверка, можно ли удалять монету
        /// </summary>
        /// <param name="coin"></param>
        /// <returns></returns>
        public bool CanRemoveCoin(int value, int count)
        {
            CoinSet coin = Coins.Where(c => c.Value == value).FirstOrDefault();

            if (coin == null)
                return false;

            if (coin.Count < count)
                return false;

            return true;
        }

        /// <summary>
        /// Очистка кошелька
        /// </summary>
        public void Clear()
        {
            Coins = new ObservableCollection<CoinSet>();
            NotifyPropertyChanged("Total");
        }

        /// <summary>
        /// Общее количество монет в кошельке
        /// </summary>
        public int CoinsCount
        {
            get { return Coins.Sum(coin => coin.Count); }
        }

        /// <summary>
        ///     Общая сумма в кошельке
        /// </summary>
        public int Total
        {
            get { return Coins.Sum(coin => coin.Count*coin.Value); }
        }
    }
}

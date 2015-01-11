using CoffeVendingMaсhine.Model;
using Rikrop.Core.Wpf.Commands;

namespace CoffeVendingMaсhine.ViewModels.Contracts
{
    public interface IMoneyOperableViewModel
    {
        void AddCoins(Wallet wallet);

        /// <summary>
        /// Основной кошелек
        /// </summary>
        Wallet Wallet { get; }

        /// <summary>
        ///     Монетоприемник
        /// </summary>
        Wallet CoinBox { get; set; }

        /// <summary>
        ///     Сдача
        /// </summary>
        Wallet Change { get; set; }
    }
}

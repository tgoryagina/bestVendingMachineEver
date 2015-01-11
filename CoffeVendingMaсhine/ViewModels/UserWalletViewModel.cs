using CoffeVendingMaсhine.Model;
using CoffeVendingMaсhine.ViewModels.Contracts;
using CoffeVendingMaсhine.Views;
using Rikrop.Core.Wpf.Commands;

namespace CoffeVendingMaсhine.ViewModels
{
    public class UserWalletViewModel : MoneyOperableViewModelBase<UserWalletView>, IUserWalletViewModel
    {
        #region Поля

        private readonly RelayCommand<CoinSet> _putCoinToBoxCommand;

        #endregion Поля

        #region Конструктор

        public UserWalletViewModel(ICoinsListViewModel coinsListViewModel) :
            base(coinsListViewModel, true)
        {
            _putCoinToBoxCommand = new RelayCommand<CoinSet>(PutCoinToBox, CanPutCoinToBox);
        }

        #endregion Конструктор

        #region Свойства

        public RelayCommand<CoinSet> PutCoinToBoxCommand
        {
            get { return _putCoinToBoxCommand; }
        }

        #endregion Свойства

        #region Обработчики команд

        public void PutCoinToBox(CoinSet coin)
        {
            Wallet.RemoveCoins(coin.Value, 1);
            CoinBox.AddCoins(coin.Value, 1);
            InvalidateCommands();
        }

        private bool CanPutCoinToBox(CoinSet coin)
        {
            return Wallet.CanRemoveCoin(coin.Value, 1);
        }

        protected override void GetChange(Wallet wallet)
        {
            Wallet.AddWallet(wallet);
            InvalidateCommands();
        }

        #endregion Обработчики команд

        #region Закрытые методы

        private void InvalidateCommands()
        {
            PutCoinToBoxCommand.InvalidateCommand();
        }

        /// <summary>
        ///     Инициализация начально состояния кошелька
        /// </summary>
        protected override Wallet InitWallet()
        {
            var wallet = new Wallet();
            wallet.AddCoins(1, 10);
            wallet.AddCoins(2, 30);
            wallet.AddCoins(5, 20);
            wallet.AddCoins(10, 15);
            return wallet;
        }

        #endregion Закрытые методы
    }
}

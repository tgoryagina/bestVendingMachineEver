using System.ComponentModel;
using System.Windows;
using CoffeVendingMaсhine.CompositeCommands;
using CoffeVendingMaсhine.Model;
using CoffeVendingMaсhine.ViewModels.Contracts;
using Microsoft.Practices.Composite.Presentation.Commands;
using Rikrop.Core.Wpf.Mvvm;

namespace CoffeVendingMaсhine.ViewModels
{
    /// <summary>
    ///     Базовая VM, содержащая список монет
    /// </summary>
    public abstract class MoneyOperableViewModelBase<TView> : Workspace<TView>
        where TView : FrameworkElement, new()
    {
        #region Поля

        private readonly ICoinsListViewModel _coinsListViewModel;

        #endregion Поля

        #region Конструктор

        protected MoneyOperableViewModelBase(ICoinsListViewModel coinsListViewModel, bool needToOperateCoinsByUI)
        {
            _coinsListViewModel = coinsListViewModel;
            _coinsListViewModel.CanOperateCoinsFromUI = needToOperateCoinsByUI;
            Wallet = InitWallet();
            Commands.GetChangeCommand.RegisterCommand(new DelegateCommand<Wallet>(GetChange, CanGetChange));
        }

        #endregion Конструктор

        #region Свойства

        public ICoinsListViewModel CoinsListViewModel
        {
            get { return _coinsListViewModel; }
        }

        /// <summary>
        ///     Основной кошелек модели для вывода списка монет
        /// </summary>
        public Wallet Wallet { get; private set; }

        /// <summary>
        ///     Монетоприемник
        /// </summary>
        public Wallet CoinBox
        {
            get { return _coinBox; }
            set
            {
                if (_coinBox != null)
                    _coinBox.PropertyChanged -= CoinBoxOnPropertyChanged;

                _coinBox = value;
                if (_coinBox != null)
                    _coinBox.PropertyChanged += CoinBoxOnPropertyChanged;
            }
        }

        private Wallet _coinBox;

        /// <summary>
        ///     Сдача
        /// </summary>
        public Wallet Change { get; set; }

        #endregion Свойства

        #region Открытые методы

        /// <summary>
        ///     Метод помещения монет в основной кошелек
        /// </summary>
        /// <param name="wallet"></param>
        public void AddCoins(Wallet wallet)
        {
            foreach (CoinSet coin in wallet.Coins)
                Wallet.AddCoins(coin.Value, coin.Count);
        }

        #endregion Открытые методы

        #region реализация Workspace

        public override string DisplayName
        {
            get { return string.Empty; }
        }

        #endregion реализация Workspace

        #region Обработка событий

        protected virtual void CoinBoxOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
        }

        protected virtual void GetChange(Wallet wallet)
        {
        }

        protected virtual bool CanGetChange(Wallet wallet)
        {
            return true;
        }

        #endregion Обработка событий

        #region Закрытые методы

        /// <summary>
        ///     Инициализация начально состояния кошелька
        /// </summary>
        protected abstract Wallet InitWallet();

        #endregion Закрытые методы
    }
}

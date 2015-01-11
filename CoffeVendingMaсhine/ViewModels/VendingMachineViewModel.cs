using System.ComponentModel;
using System.Windows;
using CoffeVendingMaсhine.Calculator.Contracts;
using CoffeVendingMaсhine.CompositeCommands;
using CoffeVendingMaсhine.Model;
using CoffeVendingMaсhine.ViewModels.Contracts;
using CoffeVendingMaсhine.Views;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace CoffeVendingMaсhine.ViewModels
{
    public class VendingMachineViewModel : MoneyOperableViewModelBase<VendingMachineView>, IVendingMachineViewModel
    {
        #region Поля

        private readonly IDrinksListViewModel _drinksListViewModel;
        private readonly IChangeCalculator _changeCalculator;
        private int _changeRest; // Остаток от сдачи после покупки

        #endregion Поля

        #region Конструктор

        public VendingMachineViewModel(ICoinsListViewModel coinsListViewModel, IDrinksListViewModel drinksListViewModel,
                                       IChangeCalculator changeCalculator)
            : base(coinsListViewModel, false)
        {
            _drinksListViewModel = drinksListViewModel;
            _changeCalculator = changeCalculator;
            Commands.BuyDrinkCommand.RegisterCommand(new DelegateCommand<Drink>(BuyDrink, CanBuyDrink));
        }

        #endregion Конструктор

        #region Свойства

        /// <summary>
        ///     Список напитков
        /// </summary>
        public IDrinksListViewModel DrinksListViewModel
        {
            get { return _drinksListViewModel; }
        }

        #endregion Свойства

        #region Обработка событий и команд

        protected override void CoinBoxOnPropertyChanged(object sender,
                                                         PropertyChangedEventArgs e)
        {
            if (CoinBox == null || CoinBox.Total == 0)
                return;
            
            var changedWallet = new Wallet();
            changedWallet.AddWallet(Wallet);

            Change.Clear();
            Wallet foundChange = FindChange(changedWallet, CoinBox.Total + _changeRest);

            if (foundChange == null || foundChange.CoinsCount == 0)
                Change.AddWallet(CoinBox);
            else
                Change.AddWallet(foundChange);
        }

        /// <summary>
        ///     Получение сдачи
        /// </summary>
        /// <param name="wallet"></param>
        protected override void GetChange(Wallet wallet)
        {
            Wallet.AddWallet(CoinBox);
            CoinBox.Clear();
            Wallet.RemoveWallet(Change);
            Change.Clear();
            _changeRest = 0;
        }

        /// <summary>
        ///     Проверка возможности получить сдачу
        /// </summary>
        /// <param name="wallet"></param>
        /// <returns></returns>
        protected override bool CanGetChange(Wallet wallet)
        {
            return Change != null && Change.Total > 0;
        }

        /// <summary>
        ///     Покупка напитка
        /// </summary>
        /// <param name="drink"></param>
        private void BuyDrink(Drink drink)
        {
            // Проверка возможности выдачи сдачи и предупреждение пользователя
            var changeAfterBuy = FindChange(Wallet, drink.Price);
            if (changeAfterBuy == null || changeAfterBuy.CoinsCount == 0)
            {
                var button = MessageBoxButton.YesNo;
                var result = MessageBox.Show("В автомате недостаточно монет для сдачи, продолжить?",
                                             "Недостаточно монет для сдачи", button);

                if (result == MessageBoxResult.No)
                    return;
            }

            Wallet.AddWallet(CoinBox);
            CoinBox.Clear();

            int changeValue = Change.Total - drink.Price;

            Change.Clear();
            var changedWallet = new Wallet();
            changedWallet.AddWallet(Wallet);
            Change.AddWallet(FindChange(changedWallet, changeValue));
            _changeRest = Change.Total;

            MessageBox.Show("Спасибо!");
        }

        /// <summary>
        ///     Проверка возможности покупки напитка
        /// </summary>
        /// <param name="drink"></param>
        /// <returns></returns>
        private bool CanBuyDrink(Drink drink)
        {
            return drink != null && Change.Total >= drink.Price;
        }

        #endregion Обработка событий и команд

        #region Закрытые методы

        /// <summary>
        ///     Инициализация начально состояния основного кошелька с монетами
        /// </summary>
        protected override Wallet InitWallet()
        {
            var wallet = new Wallet();
            wallet.AddCoins(1, 100);
            wallet.AddCoins(2, 100);
            wallet.AddCoins(5, 100);
            wallet.AddCoins(10, 100);
            return wallet;
        }

        /// <summary>
        ///     Алгоритм поиска сдачи минимальным количеством монет
        /// </summary>
        /// <param name="wallet"></param>
        /// <param name="summ"></param>
        private Wallet FindChange(Wallet wallet, int summ)
        {
            return _changeCalculator.FindChange(wallet, summ);
        }

        #endregion Закрытые методы
    }
}
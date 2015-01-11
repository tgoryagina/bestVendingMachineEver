using CoffeVendingMaсhine.Calculator;
using CoffeVendingMaсhine.CompositeCommands;
using CoffeVendingMaсhine.Model;
using CoffeVendingMaсhine.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    /// <summary>
    ///     Summary description for MainViewModelTest
    /// </summary>
    [TestClass]
    public class MainViewModelTest
    {
        public MainViewModelTest()
        {
        }

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        private MainWindowViewModel InitViewModels()
        {
                        var coinsList = new CoinsListViewModel();
            var drinksList = new DrinksListViewModel();
            var userWalletViewModel = new UserWalletViewModel(coinsList);
            var calculator = new ChangeCalculator();
            var vendingMachineViewModel = new VendingMachineViewModel(coinsList, drinksList, calculator);

            return new MainWindowViewModel(userWalletViewModel, vendingMachineViewModel);
        }

        [TestMethod]
        public void TestAddCoinToBox()
        {
            var mainViewModel = InitViewModels();

            int total = mainViewModel.UserWalletViewModel.Wallet.Total + mainViewModel.VendingMachineViewModel.Wallet.Total;
            int totalCount = mainViewModel.UserWalletViewModel.Wallet.CoinsCount + mainViewModel.VendingMachineViewModel.Wallet.CoinsCount;

            ((UserWalletViewModel)mainViewModel.UserWalletViewModel).PutCoinToBox(new CoinSet(10, 1));

            // Общее количество монет в системе не изменилось
            Assert.IsTrue(mainViewModel.UserWalletViewModel.Wallet.Total + mainViewModel.VendingMachineViewModel.Wallet.Total + mainViewModel.VendingMachineViewModel.CoinBox.Total == total);
           
            // сумма сдачи равна сумме в монетоприемнике
            Assert.IsTrue(mainViewModel.VendingMachineViewModel.Change.Total == mainViewModel.VendingMachineViewModel.CoinBox.Total);
            
            // общая сумма в системе не изменилась
            Assert.IsTrue(
                mainViewModel.UserWalletViewModel.Wallet.CoinsCount + mainViewModel.VendingMachineViewModel.Wallet.CoinsCount + mainViewModel.VendingMachineViewModel.CoinBox.CoinsCount == totalCount);
        }

        /// <summary>
        /// Проверка возможности покупки напитка
        /// </summary>
        [TestMethod]
        public void TestCanBuyDrink()
        {
            var mainViewModel = InitViewModels();

            ((UserWalletViewModel)mainViewModel.UserWalletViewModel).PutCoinToBox(new CoinSet(10, 1));

            var canBuy = Commands.BuyDrinkCommand.CanExecute(new Drink("Кофе с молоком", 21,1));

            // Нельзя купить напиток - недостаточно средств
            Assert.IsFalse(canBuy);

            ((UserWalletViewModel)mainViewModel.UserWalletViewModel).PutCoinToBox(new CoinSet(10, 1));
            ((UserWalletViewModel)mainViewModel.UserWalletViewModel).PutCoinToBox(new CoinSet(10, 1));

            canBuy = Commands.BuyDrinkCommand.CanExecute(new Drink("Кофе с молоком", 21, 1));

            // Средств достаточно, купить можно
            Assert.IsTrue(canBuy);
        }

        /// <summary>
        /// Покупка напитка
        /// </summary>
        [TestMethod]
        public void TestBuyDrink()
        {
            var mainViewModel = InitViewModels();

            int total = mainViewModel.UserWalletViewModel.Wallet.Total + mainViewModel.VendingMachineViewModel.Wallet.Total;
            int totalCount = mainViewModel.UserWalletViewModel.Wallet.CoinsCount + mainViewModel.VendingMachineViewModel.Wallet.CoinsCount;

            ((UserWalletViewModel)mainViewModel.UserWalletViewModel).PutCoinToBox(new CoinSet(10, 1));
            ((UserWalletViewModel)mainViewModel.UserWalletViewModel).PutCoinToBox(new CoinSet(10, 1));
            ((UserWalletViewModel)mainViewModel.UserWalletViewModel).PutCoinToBox(new CoinSet(10, 1));

            var canBuy = Commands.BuyDrinkCommand.CanExecute(new Drink("Кофе с молоком", 21, 1));
            Assert.IsTrue(canBuy);

            Commands.BuyDrinkCommand.Execute(new Drink("Кофе с молоком", 21, 1));

            // Общее количество монет в системе не изменилось
            Assert.IsTrue(mainViewModel.UserWalletViewModel.Wallet.Total + mainViewModel.VendingMachineViewModel.Wallet.Total + mainViewModel.VendingMachineViewModel.CoinBox.Total == total);

            // общая сумма в системе не изменилась
            Assert.IsTrue(
                mainViewModel.UserWalletViewModel.Wallet.CoinsCount + mainViewModel.VendingMachineViewModel.Wallet.CoinsCount + mainViewModel.VendingMachineViewModel.CoinBox.CoinsCount == totalCount);

            // сумма сдачи равна 9
            Assert.IsTrue(mainViewModel.VendingMachineViewModel.Change.Total == 9);
        }

        /// <summary>
        /// Покупка напитка с последующим добавлением монеты
        /// </summary>
        [TestMethod]
        public void TestBuyDrinkAndAddCoins()
        {
            var mainViewModel = InitViewModels();

            int total = mainViewModel.UserWalletViewModel.Wallet.Total + mainViewModel.VendingMachineViewModel.Wallet.Total;
            int totalCount = mainViewModel.UserWalletViewModel.Wallet.CoinsCount + mainViewModel.VendingMachineViewModel.Wallet.CoinsCount;

            ((UserWalletViewModel)mainViewModel.UserWalletViewModel).PutCoinToBox(new CoinSet(10, 1));
            ((UserWalletViewModel)mainViewModel.UserWalletViewModel).PutCoinToBox(new CoinSet(10, 1));
            ((UserWalletViewModel)mainViewModel.UserWalletViewModel).PutCoinToBox(new CoinSet(10, 1));

            var canBuy = Commands.BuyDrinkCommand.CanExecute(new Drink("Кофе с молоком", 21, 1));
            Assert.IsTrue(canBuy);

            Commands.BuyDrinkCommand.Execute(new Drink("Кофе с молоком", 21, 1));

            ((UserWalletViewModel)mainViewModel.UserWalletViewModel).PutCoinToBox(new CoinSet(5, 1));

            // Общее количество монет в системе не изменилось
            Assert.IsTrue(mainViewModel.UserWalletViewModel.Wallet.Total + mainViewModel.VendingMachineViewModel.Wallet.Total + mainViewModel.VendingMachineViewModel.CoinBox.Total == total);

            // общая сумма в системе не изменилась
            Assert.IsTrue(
                mainViewModel.UserWalletViewModel.Wallet.CoinsCount + mainViewModel.VendingMachineViewModel.Wallet.CoinsCount + mainViewModel.VendingMachineViewModel.CoinBox.CoinsCount == totalCount);

            // сумма сдачи равна 14 (9 остатка и 5 внесенных)
            Assert.IsTrue(mainViewModel.VendingMachineViewModel.Change.Total == 14);
        }
    
    }
}

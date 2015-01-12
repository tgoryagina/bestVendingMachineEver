using CoffeVendingMaсhine.Model;
using CoffeVendingMaсhine.ViewModels.Contracts;
using CoffeVendingMaсhine.Views;
using Rikrop.Core.Wpf.Mvvm;

namespace CoffeVendingMaсhine.ViewModels
{
    /// <summary>
    ///     Вью-модель основного окна, содержащая в себе вью-модель пользователя и вью-модель кофейного автомата
    /// </summary>
    public class MainWindowViewModel : Workspace<MainWindowView>, IMainWindowViewModel
    {
        #region Поля

        private readonly IUserWalletViewModel _userWalletViewModel;
        private readonly IVendingMachineViewModel _vendingMachineViewModel;

        #endregion Поля

        #region Конструктор

        public MainWindowViewModel(IUserWalletViewModel userWalletViewModel,
                                   IVendingMachineViewModel vendingMachineViewModel)
        {
            _userWalletViewModel = userWalletViewModel;
            _vendingMachineViewModel = vendingMachineViewModel;

            var coinBox = new Wallet();
            _userWalletViewModel.CoinBox = _vendingMachineViewModel.CoinBox = coinBox;

            var change = new Wallet();
            _userWalletViewModel.Change = _vendingMachineViewModel.Change = change;
        }

        #endregion Конструктор

        #region Свойства

        public IUserWalletViewModel UserWalletViewModel
        {
            get { return _userWalletViewModel; }
        }

        public IVendingMachineViewModel VendingMachineViewModel
        {
            get { return _vendingMachineViewModel; }
        }

        #endregion Свойства

        #region реализация Workspace

        public override string DisplayName
        {
            get { return "Validation sample application"; }
        }

        #endregion реализация Workspace
    }
}

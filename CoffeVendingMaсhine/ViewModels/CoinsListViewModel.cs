using CoffeVendingMaсhine.ViewModels.Contracts;
using CoffeVendingMaсhine.Views;
using Rikrop.Core.Wpf.Mvvm;

namespace CoffeVendingMaсhine.ViewModels
{
    /// <summary>
    ///     Вью-модель списка монет
    /// </summary>
    public class CoinsListViewModel : Workspace<CoinsListView>, ICoinsListViewModel
    {
        #region Свойства

        public bool CanOperateCoinsFromUI { get; set; }

        #endregion Свойства

        #region реализация Workspace

        public override string DisplayName
        {
            get { return "Список доступных монет"; }
        }

        #endregion реализация Workspace
    }
}

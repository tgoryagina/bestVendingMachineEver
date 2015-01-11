using System.Collections.ObjectModel;
using CoffeVendingMaсhine.CompositeCommands;
using CoffeVendingMaсhine.Model;
using CoffeVendingMaсhine.ViewModels.Contracts;
using CoffeVendingMaсhine.Views;
using Microsoft.Practices.Composite.Presentation.Commands;
using Rikrop.Core.Wpf.Mvvm;

namespace CoffeVendingMaсhine.ViewModels
{
    public class DrinksListViewModel : Workspace<DrinksListView>, IDrinksListViewModel
    {
        #region Конструктор

        public DrinksListViewModel()
        {
            Drinks = InitDrinks();
            Commands.BuyDrinkCommand.RegisterCommand(new DelegateCommand<Drink>(BuyDrink, CanBuyDrink));
        }

        #endregion Конструктор

        #region Свойства

        public ObservableCollection<Drink> Drinks { get; private set; }

        #endregion Свойства

        #region Закрытые методы

        private ObservableCollection<Drink> InitDrinks()
        {
            var drinks = new ObservableCollection<Drink>();
            drinks.Add(new Drink("Чай", 13, 10));
            drinks.Add(new Drink("Кофе", 18, 20));
            drinks.Add(new Drink("Кофе с молоком", 21, 20));
            drinks.Add(new Drink("Сок", 35, 15));

            return drinks;
        }

        private void BuyDrink(Drink drink)
        {
            drink.Count--;
        }

        private bool CanBuyDrink(Drink drink)
        {
            return drink != null && Drinks != null && drink.Count > 0;
        }

        #endregion Закрытые методы

        #region Workspace

        public override string DisplayName
        {
            get { return string.Empty; }
        }

        #endregion Workspace
    }
}
using Microsoft.Practices.Composite.Presentation.Commands;

namespace CoffeVendingMaсhine.CompositeCommands
{
    public static class Commands
    {
        /// <summary>
        /// Команда получения сдачи
        /// </summary>
        public static CompositeCommand GetChangeCommand = new CompositeCommand();

        /// <summary>
        /// Команда покупки напитка
        /// </summary>
        public static CompositeCommand BuyDrinkCommand = new CompositeCommand();
    }
}
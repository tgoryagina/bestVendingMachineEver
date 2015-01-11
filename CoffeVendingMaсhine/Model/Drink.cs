using System;
using Rikrop.Core.Wpf;

namespace CoffeVendingMaсhine.Model
{
    /// <summary>
    ///     Напиток, знающий свое название и количество
    /// </summary>
    public class Drink : ChangeNotifier
    {
        public Drink(string name, int price, int count)
        {
            Name = name;
            Price = price;
            Count = count;
        }

        #region Свойства

        public string Name { get; private set; }

        public int Price { get; private set; }

        public int Count
        {
            get { return _count; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Количество напитков не может быть отрицательным");
                SetProperty(ref _count, value);
            }
        }

        private int _count;

        #endregion Свойства
    }
}

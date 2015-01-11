using System;
using Rikrop.Core.Wpf;

namespace CoffeVendingMaсhine.Model
{
    /// <summary>
    ///     "Набор" монет, знающий свой номинал и количество
    /// </summary>
    public class CoinSet : ChangeNotifier
    {
        public CoinSet(int value, int count)
        {
            Value = value;
            Count = count;
        }

        #region Свойства

        public int Value { get; private set; }

        public int Count
        {
            get { return _count; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Количество монет в наборе не может быть отрицательным"); //TODO: ы?
                SetProperty(ref _count, value);
            }
        }
        private int _count;

        #endregion Свойства
    }
}

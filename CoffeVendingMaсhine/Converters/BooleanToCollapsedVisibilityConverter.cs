using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CoffeVendingMaсhine.Converters
{
    /// <summary>
    ///     Конвертер булевого значения в Visibility, причем при false возвращается Visibility.Collapsed в отличие от стандартного конвертера
    /// </summary>
    [ValueConversion(typeof (bool), typeof (Visibility))]
    public sealed class BooleanToCollapsedVisibilityConverter : IValueConverter
    {
        #region Реализация IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof (Visibility) && targetType != typeof (object))
            {
                // TODO: перенести в ресурсы
                throw new InvalidOperationException("The target must be a string or object");
            }

            return ((bool) value ? Visibility.Visible : Visibility.Collapsed);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion Реализация IValueConverter
    }
}

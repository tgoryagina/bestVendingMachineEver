using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using CoffeVendingMaсhine.App;

namespace CoffeVendingMaсhine
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            StartClient(); 
        }

        /// <summary>
        ///     Запуск клиентского приложения.
        /// </summary>
        public static void StartClient()
        {
            try
            {
                ConfigureGlobalSettings();

                using (var container = new ClientContainer())
                {
                    App.App app = container.CreateApp();
                    app.Run();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        ///     Задание глобальных свойств приложения.
        /// </summary>
        private static void ConfigureGlobalSettings()
        {
            var russianCulture = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentUICulture = russianCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru");
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof (FrameworkElement),
                                                               new FrameworkPropertyMetadata(
                                                                   XmlLanguage.GetLanguage(
                                                                       russianCulture.IetfLanguageTag)));
            FrameworkContentElement.LanguageProperty.OverrideMetadata(typeof (TextElement),
                                                                      new FrameworkPropertyMetadata(
                                                                          XmlLanguage.GetLanguage(
                                                                              russianCulture.IetfLanguageTag)));
        }
    }
}

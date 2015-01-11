using System.Windows;
using System.Windows.Threading;
using CoffeVendingMaсhine.ViewModels;

namespace CoffeVendingMaсhine.App
{
    public class App : Application
    {
        /// <summary>
        ///     ViewModel главного окна приложения.
        /// </summary>
        private readonly MainWindowViewModel _mainWindowViewModel;

        public App(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            ConfigureApplication();
        }

        /// <summary>
        ///     Задание общих свойств приложения.
        /// </summary>
        private void ConfigureApplication()
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
            Startup += (sender, args) => ShowMainWindow();
            DispatcherUnhandledException += OnUnhandledException;
        }

        /// <summary>
        ///     Отображение главного окна приложения.
        /// </summary>
        private void ShowMainWindow()
        {
            MainWindow = _mainWindowViewModel.TypedContent;
            _mainWindowViewModel.TypedContent.Closed += (sender, args) => ShutdownApplication();
            _mainWindowViewModel.TypedContent.Show();
        }

        /// <summary>
        ///     Закрытие приложения.
        /// </summary>
        private void ShutdownApplication()
        {
            Shutdown();
        }

        /// <summary>
        ///     Обработчик всех пропущенных исключений в приложении.
        /// </summary>
        private void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            MessageBox.Show(args.Exception.Message);
        }
    }
}

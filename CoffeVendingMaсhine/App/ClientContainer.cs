using System;
using System.Configuration;
using CoffeVendingMaсhine.Calculator;
using CoffeVendingMaсhine.Calculator.Contracts;
using CoffeVendingMaсhine.ViewModels;
using CoffeVendingMaсhine.ViewModels.Contracts;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace CoffeVendingMaсhine.App
{
    /// <summary>
    ///     Заргузчик сервисов и служб.
    /// </summary>
    public class ClientContainer : IDisposable
    {
        /// <summary>
        ///     Родительский контейнер приложения.
        /// </summary>
        private IUnityContainer _container;

        /// <summary>
        ///     Конструктор
        /// </summary>
        public ClientContainer()
        {
            InitializeServiceLocator();
        }

        /// <summary>
        ///     Инициализация ServiceLocator
        /// </summary>
        private void InitializeServiceLocator()
        {
            var locator = new UnityServiceLocator(CreateContainer());
            ServiceLocator.SetLocatorProvider(() => locator);
        }

        /// <summary>
        ///     Получение экземпляра приложения.
        /// </summary>
        /// <returns>Экземпляр приложения.</returns>
        public App CreateApp()
        {
            return _container.Resolve<App>();
        }

        /// <summary>
        ///     Создание контейнера.
        /// </summary>
        /// <returns>контейнер.</returns>
        private IUnityContainer CreateContainer()
        {
            _container = new UnityContainer();
            var section = (UnityConfigurationSection) ConfigurationManager.GetSection("unity");
            _container.LoadConfiguration(section);

            #region Singleton

            _container.RegisterType<IChangeCalculator, ChangeCalculator>(new ContainerControlledLifetimeManager());

            #endregion Singleton

            #region Instance

            _container.RegisterType<IMainWindowViewModel, MainWindowViewModel>();
            _container.RegisterType<ICoinsListViewModel, CoinsListViewModel>();
            _container.RegisterType<IUserWalletViewModel, UserWalletViewModel>();
            _container.RegisterType<IVendingMachineViewModel, VendingMachineViewModel>();
            _container.RegisterType<IDrinksListViewModel, DrinksListViewModel>();

            #endregion Instance

            return _container;
        }

        public void Dispose()
        {
            if (_container != null)
            {
                _container.Dispose();
            }
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using WeatherMonitoringSystem.Bootstrapping;
using WeatherMonitoringSystem.Infraestructure.Horizontal.Services;
using WeatherMonitoringSystem.Presentation.Services.Navigation;
using WeatherMonitoringSystem.Presentation.Services.Navigation.Extensions;
using INavigationService = WeatherMonitoringSystem.Presentation.Services.Navigation.INavigationService;
using SW = System.Windows;
// Suppress entire file

namespace WeatherMonitoringSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : SW.Application
    {
        private static IServiceProvider _serviceProvider;

        private readonly IHost _host;

        public App()
        {

        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            AppContainer.RegisterTypes();
            AppContainer.Resolve<INavigationService>()?.Show(nameof(MainWindow));
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppContainer.Dispose();
            base.OnExit(e);
        }
    }
}

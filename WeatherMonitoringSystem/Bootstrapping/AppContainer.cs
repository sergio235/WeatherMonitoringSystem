using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices.JavaScript;
using System.Windows;
using WeatherMonitoringSystem.Infraestructure.Horizontal.Services;
using WeatherMonitoringSystem.Presentation.Services.Navigation;
using WeatherMonitoringSystem.Presentation.Services.Navigation.Extensions;
using WeatherMonitoringSystem.Presentation.ViewModels;
using WeatherMonitoringSystem.Presentation.Views;

namespace WeatherMonitoringSystem.Bootstrapping
{
    public static class AppContainer
    {
        private static IHost _host;
        private static IServiceProvider _servicesProvider;

        public static void RegisterTypes()
        {
            _host = Host.CreateDefaultBuilder()
                        .ConfigureAppConfiguration((hostContext, configBuilder) =>
                        {
                            // Configura el archivo appsettings.json
                            configBuilder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                        })
                        .ConfigureServices((context, services) =>
                        {
                            // Configura el servicio de Aemet
                            services.Configure<AemetServiceConfig>(context.Configuration.GetSection("AemetServiceConfig"));
                            services.AddTransient(typeof(IAemetService), typeof(AemetService));

                            services.AddViews();
                            services.AddViewModels();
                            services.AddSingleton<INavigationService>(serviceProvider =>
                            {
                                var navigationService = new NavigationService(serviceProvider, nameof(MainWindow));
                                return navigationService.ConfigureViews();
                            });
                        })
                        .Build();

            _servicesProvider = _host.Services;
        }

        public static object? Resolve(Type typeName)
        {
            return _servicesProvider.GetService(typeName);
        }

        public static T? Resolve<T>()
        {
            return _servicesProvider.GetService<T>();
        }

        public static async Task Dispose()
        {
            await _host.StopAsync();
            _host.Dispose();
        }

        private static void AddViews(this IServiceCollection services)
        {
            services.AddScoped<MainWindow>();
            services.AddTransient<AltamarWindow>();
            services.AddTransient<CosteraWindow>();
        }

        private static void AddViewModels(this IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<AltamarWindowViewModel>();
            services.AddTransient<CosteraWindowViewModel>();
        }
    }
}

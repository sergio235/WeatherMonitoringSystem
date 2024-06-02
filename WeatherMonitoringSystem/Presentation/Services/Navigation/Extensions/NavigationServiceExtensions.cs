using WeatherMonitoringSystem.Presentation.Views;
using WeatherMonitoringSystem.Presentation.Services.Navigation;

namespace WeatherMonitoringSystem.Presentation.Services.Navigation.Extensions
{
    /// <summary>
    /// NavigationServiceCollection
    /// </summary>
    public static class NavigationServiceCollection
    {
        /// <summary>
        /// Configures the views.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <returns></returns>
        public static INavigationService ConfigureViews(this INavigationService navigationService)
        {
            navigationService.Configure(typeof(MainWindow));
            navigationService.Configure(typeof(CosteraWindow));
            navigationService.Configure(typeof(AltamarWindow));

            return navigationService;
        }
    }
}
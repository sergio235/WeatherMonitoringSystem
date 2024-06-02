using SW = System.Windows;

namespace WeatherMonitoringSystem.Presentation.Services.Navigation
{
    public interface INavigationService
    {
        string RootName { get; }
        SW.Window Current { get; }
        SW.Window MainWindow { get; }
        void Configure(string key, Type windowType);

        void Configure(Type windowType);

        void Show(string windowKey, object? parameter = null);

        bool? ShowDialog(string windowKey, object? parameter = null);
    }
}
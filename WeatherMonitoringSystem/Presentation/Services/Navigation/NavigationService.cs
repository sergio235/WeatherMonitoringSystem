using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeatherMonitoringSystem.Base;
using SW = System.Windows;

namespace WeatherMonitoringSystem.Presentation.Services.Navigation
{
    //https://marcominerva.wordpress.com/2020/01/13/an-mvvm-aware-navigationservice-for-wpf-running-on-net-core/
    /// <summary>
    /// Navigation Service Class
    /// </summary>
    /// <seealso cref="Hermes.Shared.Presentation.Services.Interfaces.INavigationService" />
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// Gets the _windows.
        /// </summary>
        /// <value>
        /// The _windows.
        /// </value>
        private Dictionary<string, Type> _windows { get; } = new Dictionary<string, Type>();
        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;
        /// <summary>
        /// The navigation stack
        /// </summary>
        private readonly Stack<SW.Window> _navigationStack = new Stack<SW.Window>();
        private readonly string _rootName;
        public string RootName => _rootName;
        public SW.Window Current => _navigationStack.Peek();
        public SW.Window MainWindow => _navigationStack.First(x => x.Name == RootName);

        /// <summary>
        /// Configures the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="windowType">Type of the window.</param>
        public void Configure(string key, Type windowType)
            => _windows.Add(key, windowType);

        /// <summary>
        /// Configures the specified window type.
        /// </summary>
        /// <param name="windowType">Type of the window.</param>
        public void Configure(Type windowType)
            => Configure(windowType.Name, windowType);

        /// <summary>Initializes a new instance of the <see cref="NavigationService" /> class.</summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="logger"></param>
        public NavigationService(IServiceProvider serviceProvider, string rootName)
        {
            _serviceProvider = serviceProvider;
            _rootName = rootName;
            _logger = _serviceProvider.GetRequiredService<ILogger<INavigationService>>();
        }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="windowKey">The window key.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public bool? ShowDialog(string windowKey,
            object? parameter = null)
        {
            SW.Window window = GetAndActivateWindow(windowKey, parameter);

            _navigationStack.Push(window);

            return window.ShowDialog();
        }

        /// <summary>
        /// Shows the specified windowkey.
        /// </summary>
        /// <param name="windowkey">The windowkey.</param>
        /// <param name="parameter">The parameter.</param>
        public void Show(string windowkey, object? parameter = null)
        {
            var window = GetAndActivateWindow(windowkey, parameter);

            if (_navigationStack.Count > 0)
                _navigationStack.Peek()?.Hide();

            _navigationStack.Push(window);

            try
            {
                window.Show();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Se ha producido un error al abrir la ventana [{windowkey}]");
            }

        }

        /// <summary>
        /// Gets the and activate window.
        /// </summary>
        /// <param name="windowKey">The window key.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        private SW.Window GetAndActivateWindow(string windowKey, object? parameter = null)
        {
            try
            {
                var window = _serviceProvider.GetRequiredService(_windows[windowKey]) as SW.Window;

                if (window?.DataContext is IActivable activable)
                {
                    activable.Activate(parameter);

                    if (window.DataContext is ViewModelBase viewModel && viewModel.Activated)
                    {
                        viewModel.InitVariablesAfterActivating();
                    }
                }
                return window;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener y activar la ventana {windowKey}");
            }

            return null;
        }
    }
}
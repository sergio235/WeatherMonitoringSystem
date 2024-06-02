using System.Diagnostics.CodeAnalysis;
using SW = System.Windows;

// Suppress entire file
[assembly: SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
namespace WeatherMonitoringSystem.Presentation.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : SW.Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Name = nameof(MainWindow);
        }
    }
}

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Disposables;
using WeatherMonitoringSystem.Base;
using WeatherMonitoringSystem.Infraestructure.Horizontal.Services;
using WeatherMonitoringSystem.Infraestructure.Horizontal.Services.Response;
using WeatherMonitoringSystem.Presentation.Base;
using WeatherMonitoringSystem.Presentation.Base.Extensions;
using WeatherMonitoringSystem.Presentation.Bindables;
using WeatherMonitoringSystem.Presentation.Services.Navigation;

namespace WeatherMonitoringSystem.Presentation.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private readonly INavigationService _navigationService;

        Dictionary<string, string> _optionsAndViews;

        ObservableCollection<BindableAemetOption> _aemetOptions;
        BindableAemetOption _selectedAemetOptions;

        CompositeDisposable _disposables;


        public MainWindowViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            InitVariables();
            InitEvents();
        }

        public ObservableCollection<BindableAemetOption> AemetOptions
        {
            get => _aemetOptions;
            set => SetProperty(ref _aemetOptions, value);
        }

        public BindableAemetOption SelectedAemetOptions
        {
            get => _selectedAemetOptions;
            set => SetProperty(ref _selectedAemetOptions, value);
        }

        private void InitVariables()
        {
            _optionsAndViews = new Dictionary<string, string>()
            {
                { "Altamar", "AltamarWindow" },
                { "Costera", "CosteraWindow" }
            };

            AemetOptions = new ObservableCollection<BindableAemetOption>
            {
                new BindableAemetOption{ Value = "Elija una opción"},
                new BindableAemetOption{ Value = "Altamar"},
                new BindableAemetOption{ Value = "Costera"}
            };

            SelectedAemetOptions = new BindableAemetOption { Value = "Elija una opción" };

            _disposables = new CompositeDisposable();
        }

        private void InitEvents()
        {
            _disposables = new CompositeDisposable();

            this.WhenAnyValue(x => x.SelectedAemetOptions)
                .Subscribe(x =>
                {
                    if (_optionsAndViews.ContainsKey(x.Value))
                    {
                        _navigationService.ShowDialog(_optionsAndViews[x.Value]);
                    }
                })
                .DisposeWith(_disposables);

        }

        public override void InitVariablesAfterActivating()
        {
            //SelectedAemetOptions = null;
            base.InitVariablesAfterActivating();
        }

        protected override void Close(object o)
        {
            _navigationService.Current.Close();
        }
    }
}

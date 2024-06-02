using Microsoft.Extensions.Options;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using WeatherMonitoringSystem.Base;
using WeatherMonitoringSystem.Infraestructure.Horizontal.Services;
using WeatherMonitoringSystem.Infraestructure.Horizontal.Services.Response;
using WeatherMonitoringSystem.Presentation.Base.Extensions;
using WeatherMonitoringSystem.Presentation.Bindables;
using WeatherMonitoringSystem.Presentation.Services.Navigation;
using WeatherMonitoringSystem.Presentation.Views;

namespace WeatherMonitoringSystem.Presentation.ViewModels
{
    public class AltamarWindowViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IAemetService _aemetService;
        private readonly AemetServiceConfig _config;

        private IDictionary<string, string> _altamarUrls;

        ObservableCollection<BindableAemetOption> _aemetOptions;
        BindableAemetOption _selectedAemetOptions;

        CompositeDisposable _disposables;

        ObservableCollection<AltamarDatos> _altamarDatos;
        SituacionAltamar _situacionAltamar;
        ObservableCollection<ZonaAltamar> _zonaAltamars;

        public AltamarWindowViewModel(INavigationService navigationService, IAemetService aemetService, IOptions<AemetServiceConfig> config) : base(navigationService)
        {
            _navigationService = navigationService;
            _aemetService = aemetService;
            _config = config.Value;

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

        public ObservableCollection<AltamarDatos> AltamarDatos
        {
            get => _altamarDatos;
            set => SetProperty(ref _altamarDatos, value);
        }

        public SituacionAltamar Situacion
        {
            get => _situacionAltamar;
            set => SetProperty(ref _situacionAltamar, value);
        }

        public ObservableCollection<ZonaAltamar> ZonaAltamars
        {
            get => _zonaAltamars;
            set => SetProperty(ref _zonaAltamars, value);
        }

        private void InitVariables()
        {
            _altamarUrls = _config.Urls.Altamar;
            AemetOptions = new ObservableCollection<BindableAemetOption>(_altamarUrls.Keys.Select(key => new BindableAemetOption { Value = key }));

            SelectedAemetOptions = new BindableAemetOption { Value = string.Empty };

            AltamarDatos = new ObservableCollection<AltamarDatos>();
            Situacion = new SituacionAltamar();
            ZonaAltamars = new ObservableCollection<ZonaAltamar>();
        }

        private void InitEvents()
        {
            _disposables = new CompositeDisposable();

            this.WhenAnyValue(x => x.SelectedAemetOptions)
                .Subscribe(async x =>
                {
                    if (_altamarUrls.ContainsKey(x.Value))
                    {
                        await _aemetService.GetDataAsync<AltamarDatos>(_altamarUrls[x.Value])
                        .ContinueWith(task =>
                        {
                            AltamarDatos = new ObservableCollection<AltamarDatos>(task.Result);

                            // NOTA: En los datos recuperados solo se recupera un objeto AltamarDatos,
                            // por lo que podemos tomar First, pero en una app real esto podría fallar. 
                            Situacion = AltamarDatos.Select(x => x.Situacion).First();

                            ZonaAltamars = new ObservableCollection<ZonaAltamar>(
                                AltamarDatos
                                .Select(x => x.Prediccion)
                                .SelectMany(x => x.Zona)
                                .OrderBy(x => x.Id));
                        });
                    }
                })
                .DisposeWith(_disposables);

        }

        protected override void Close(object o)
        {
            View = nameof(MainWindow);
            base.Close(o);
        }
    }
}

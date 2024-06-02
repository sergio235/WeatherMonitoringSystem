using Microsoft.Extensions.Options;
using WeatherMonitoringSystem.Base;
using WeatherMonitoringSystem.Infraestructure.Horizontal.Services.Response;
using WeatherMonitoringSystem.Infraestructure.Horizontal.Services;
using WeatherMonitoringSystem.Presentation.Services.Navigation;
using System.Collections.ObjectModel;
using WeatherMonitoringSystem.Presentation.Bindables;
using System.Reactive.Disposables;
using WeatherMonitoringSystem.Presentation.Base.Extensions;
using WeatherMonitoringSystem.Presentation.Views;

namespace WeatherMonitoringSystem.Presentation.ViewModels
{
    public class CosteraWindowViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IAemetService _aemetService;
        private readonly AemetServiceConfig _config;

        private IDictionary<string, string> _costeraUrls;

        ObservableCollection<BindableAemetOption> _aemetOptions;
        BindableAemetOption _selectedAemetOptions;

        CompositeDisposable _disposables;

        ObservableCollection<CosteraDatos> _costeraDatos;
        SituacionCostera _situacion;
        ObservableCollection<SubzonaCostera> _zonaCosteras;

        public CosteraWindowViewModel(INavigationService navigationService, IAemetService aemetService, IOptions<AemetServiceConfig> config) : base(navigationService)
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

        public ObservableCollection<CosteraDatos> CosteraDatos
        {
            get => _costeraDatos;
            set => SetProperty(ref _costeraDatos, value);
        }

        public SituacionCostera Situacion
        {
            get => _situacion;
            set => SetProperty(ref _situacion, value);
        }

        public ObservableCollection<SubzonaCostera> ZonaCosteras
        {
            get => _zonaCosteras;
            set => SetProperty(ref _zonaCosteras, value);
        }

        private void InitVariables()
        {
            _costeraUrls = _config.Urls.Costera;

            AemetOptions = new ObservableCollection<BindableAemetOption>(_costeraUrls.Keys.Select(key => new BindableAemetOption { Value = key }));

            SelectedAemetOptions = new BindableAemetOption { Value = string.Empty };

            CosteraDatos = new ObservableCollection<CosteraDatos>();
            Situacion = new SituacionCostera();
            ZonaCosteras = new ObservableCollection<SubzonaCostera>();
        }

        private void InitEvents()
        {
            _disposables = new CompositeDisposable();

            this.WhenAnyValue(x => x.SelectedAemetOptions)
                .Subscribe(async x =>
                {
                    if (_costeraUrls.ContainsKey(x.Value))
                    {
                        await _aemetService.GetDataAsync<CosteraDatos>(_costeraUrls[x.Value])
                        .ContinueWith(task =>
                        {
                            CosteraDatos = new ObservableCollection<CosteraDatos>(task.Result);

                            // NOTA: En los datos recuperados solo se recupera un objeto CosteraDatos,
                            // por lo que podemos tomar First, pero en una app real esto podría fallar. 
                            Situacion = CosteraDatos.Select(x => x.Situacion).First();

                            ZonaCosteras = new ObservableCollection<SubzonaCostera>(
                                CosteraDatos
                                .Select(x => x.Prediccion)
                                .SelectMany(x => x.Zona)
                                .SelectMany(x => x.Subzona)
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

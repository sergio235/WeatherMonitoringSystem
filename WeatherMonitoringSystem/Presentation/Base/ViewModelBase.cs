using System.ComponentModel;
using WeatherMonitoringSystem.Presentation;
using WeatherMonitoringSystem.Presentation.Base;
using WeatherMonitoringSystem.Presentation.Services.Navigation;
using BindableObject = WeatherMonitoringSystem.Presentation.Base.BindableObject;

namespace WeatherMonitoringSystem.Base
{
    public partial class ViewModelBase : BindableObject, IActivable
    {
        private readonly INavigationService _navigationService;
        private bool _activated;

        public ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitCommands();
        }

        public bool Activated
        {
            get => _activated;
            set => SetProperty(ref _activated, value);
        }

        public ExtendedBindableObject? NavigationParameter { get; set; }
        public Dictionary<string, object>? NavigationParameters { get; set; }

        public string View { get; set; }
        public RelayCommand<CancelEventArgs> ClosingCommand { get; set; }
        public RelayCommand<object> CloseCommand { get;set; }
        
        public virtual void InitCommands()
        {
            ClosingCommand = new RelayCommand<CancelEventArgs>(Closing);
            CloseCommand = new RelayCommand<object>(Close);
        }
        public virtual void InitVariablesAfterActivating()
        {
            if (Activated)
            {
                ResetNavigationParameter();
            }
        }

        public void Activate(object parameter)
        {
            Activated = false;

            if (parameter is ExtendedBindableObject)
            {
                NavigationParameter = parameter as ExtendedBindableObject;
            }

            if (parameter is Dictionary<string, object> parameters)
            {
                NavigationParameters = parameter as Dictionary<string, object>;
            }

            Activated = true;
        }

        protected virtual void Closing(object o)
        {
            if (o is CancelEventArgs cancelEventArgs)
            {
                cancelEventArgs.Cancel = false;
            }
        }

        protected virtual void Close(object o)
        {
            ResetNavigationParameter();
            _navigationService.Show(View);
        }

        private void ResetNavigationParameter()
        {
            NavigationParameter = new ExtendedBindableObject();
            NavigationParameters = new Dictionary<string, object>();
        }
    }
}

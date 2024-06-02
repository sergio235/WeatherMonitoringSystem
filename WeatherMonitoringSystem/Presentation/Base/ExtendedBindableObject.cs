using System.Runtime.CompilerServices;

namespace WeatherMonitoringSystem.Presentation.Base
{
    public class ExtendedBindableObject : BindableObject, ISuspendableNotifyPropertyChanged
    {
        private bool _isSuspended;
        private readonly HashSet<string> _changedProperties = new HashSet<string>();
        private readonly object _lockObject = new object();

        public bool IsSuspended
        {
            get { return _isSuspended; }
            set { _isSuspended = value; }
        }

        public void Pause()
        {
            lock (_lockObject)
            {
                _isSuspended = true;
            }
        }

        public void Resume()
        {
            lock (_lockObject)
            {
                _isSuspended = false;
                foreach (var propertyName in _changedProperties)
                {
                    OnPropertyChanged(propertyName);
                }
                _changedProperties.Clear();
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            lock (_lockObject)
            {
                if (!_isSuspended)
                {
                    base.OnPropertyChanged(propertyName);
                }
                else
                {
                    _changedProperties.Add(propertyName);
                }
            }
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            lock (_lockObject)
            {
                if (EqualityComparer<T>.Default.Equals(storage, value))
                {
                    return false;
                }

                storage = value;
                OnPropertyChanged(propertyName);
                return true;
            }
        }
    }
}

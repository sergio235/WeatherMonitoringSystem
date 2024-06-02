using System.Reactive.Disposables;

namespace WeatherMonitoringSystem.Presentation.Base.Extensions
{
    public static class DisposableExtensions
    {
        public static IDisposable DisposeWith(this IDisposable disposable, CompositeDisposable compositeDisposable)
        {
            if (disposable == null)
            {
                throw new ArgumentNullException(nameof(disposable));
            }

            if (compositeDisposable == null)
            {
                throw new ArgumentNullException(nameof(compositeDisposable));
            }

            compositeDisposable.Add(disposable);

            return disposable;
        }
    }
}
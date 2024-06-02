using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMonitoringSystem.Presentation
{
    internal interface IActivable
    {
        void Activate(object parameter);
    }
}

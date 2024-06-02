using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherMonitoringSystem.Presentation.Base;

namespace WeatherMonitoringSystem.Presentation.Bindables
{
    public class BindableAemetOption : BindableObject
    {
        private string _value;

        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
}

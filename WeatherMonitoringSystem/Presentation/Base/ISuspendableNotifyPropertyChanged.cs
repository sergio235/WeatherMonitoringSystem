﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMonitoringSystem.Presentation.Base
{
    public interface ISuspendableNotifyPropertyChanged : INotifyPropertyChanged
    {
        bool IsSuspended { get; set; }

        void Pause();

        void Resume();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherMonitoringSystem.Presentation.Base;

namespace WeatherMonitoringSystem.Presentation.Bindables
{
    public class BindableSituacion : BindableObject
    {
        private DateTime _inicio;
        private DateTime _fin;
        private string _texto;
        public string _id;
        private string _nombre;
        public DateTime Inicio 
        {
            get => _inicio;
            set => SetProperty(ref _inicio, value);
        }

        public DateTime Fin 
        {
            get => _fin;
            set => SetProperty(ref _fin, value);
        }

        public string Texto 
        {
            get => _texto;
            set => SetProperty(ref _texto, value);
        }
        public string Id 
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        public string Nombre 
        {
            get => _nombre;
            set => SetProperty(ref _nombre, value);
        }
    }
}

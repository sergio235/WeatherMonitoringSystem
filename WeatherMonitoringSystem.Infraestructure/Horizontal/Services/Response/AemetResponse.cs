using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMonitoringSystem.Infraestructure.Horizontal.Services.Response
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class AemetResponse
    {

        public string Descripcion { get; set; }

        public string Estado { get; set; }

        public string Datos { get; set; }

        public string Metadatos { get; set; }
    }
}

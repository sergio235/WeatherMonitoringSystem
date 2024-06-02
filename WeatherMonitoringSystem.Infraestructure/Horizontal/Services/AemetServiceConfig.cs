using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMonitoringSystem.Infraestructure.Horizontal.Services
{
    public class AemetServiceConfig
    {
        public string ApiKey { get; set; }
        public Urls Urls { get; set; }
    }

    public class Urls
    {
        public Dictionary<string, string> Altamar { get; set; }
        public Dictionary<string, string> Costera { get; set; }
    }
}

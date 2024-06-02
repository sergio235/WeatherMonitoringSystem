using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherMonitoringSystem.Infraestructure.Horizontal.Services.Response;

namespace WeatherMonitoringSystem.Infraestructure.Horizontal.Services
{
    public interface IAemetService
    {
        Task<List<IDatos>> GetDataAsync<IDatos>(string url);
    }
}

using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using WeatherMonitoringSystem.Infraestructure.Horizontal.Services.Response;

namespace WeatherMonitoringSystem.Infraestructure.Horizontal.Services
{
    public class AemetService : IAemetService
    {
        private readonly RestClient _client;
        private readonly AemetServiceConfig _config;

        public AemetService(IOptions<AemetServiceConfig> config)
        {
            _config = config.Value;
            _client = new RestClient();

            // Configura el RestClient para agregar la api-key en cada solicitud
            _client.AddDefaultHeader("Authorization", $"Bearer {_config.ApiKey}");
        }

        public async Task<List<TDatos>> GetDataAsync<TDatos>(string url)
        {
            var datosUrl = await GetUrlsAsync(url);

            var datos = await GetStringAsync(datosUrl);

            var datosResponse = JsonConvert.DeserializeObject<List<TDatos>>(datos);


            return datosResponse;
        }

        private async Task<string> GetUrlsAsync(string url)
        {
            var request = new RestRequest(url, Method.Get);
            var response = await _client.ExecuteAsync(request);

            if(string.IsNullOrEmpty(response.Content))
            {
                throw new InvalidOperationException("Error al solicitar datos a Aemet");
            }

            var aemetResponse = JsonConvert.DeserializeObject<AemetResponse>(response.Content);

            if (aemetResponse == null || string.IsNullOrEmpty(aemetResponse.Datos))
            {
                throw new InvalidOperationException("Error al obtener las URLs de datos. La respuesta es nula o no contiene las propiedades necesarias.");
            }

            return aemetResponse.Datos;
        }

        private async Task<string> GetStringAsync(string url)
        {
            var request = new RestRequest(url, Method.Get);
            var response = await _client.ExecuteAsync(request);

            if(string.IsNullOrEmpty(response.Content))
            {
                throw new InvalidOperationException("Error al obtener las URLs de datos. La respuesta es nula o no contiene las propiedades necesarias.");
            }
            return response.Content;
        }
    }
}

using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using WeatherForecast.BLL.Models.WeatherData;

namespace WeatherForecast.BLL.Services
{
    public class WeatherService(HttpClient httpClient, IConfiguration configuration)
    {
        private readonly string? ApiKey = configuration["ApiKey"];

        public Task<WeatherResponse?> Get(WeatherRequest request) => httpClient.GetFromJsonAsync<WeatherResponse>($"?lat={request.Lat}&lon={request.Lon}&appid={ApiKey}");
    }
}

using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Web;
using WeatherForecast.BLL.Interfaces;
using WeatherForecast.BLL.Models.WeatherData;

namespace WeatherForecast.BLL.Services
{
    public class WeatherService(HttpClient httpClient, IConfiguration configuration) : Service(configuration), IWeatherService
    {
        public Task<WeatherResponse?> Get(WeatherRequest request)
        {
            var parameters = HttpUtility.ParseQueryString("");

            parameters["lat"] = request.Lat.ToString();

            parameters["lon"] = request.Lon.ToString();

            parameters["appid"] = ApiKey;

            return httpClient.GetFromJsonAsync<WeatherResponse>($"?{parameters}");
        }
    }
}

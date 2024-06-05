using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using WeatherForecast.BLL.Interfaces;
using WeatherForecast.BLL.Models.WeatherData;

namespace WeatherForecast.BLL.Services
{
    public class WeatherService(HttpClient httpClient, IConfiguration configuration) : Service(configuration), IWeatherService
    {
        public Task<CurrentWeatherResponse?> GetCurrent(CurrentWeatherRequest request)
        {
            var parameters = GetParameters();

            parameters["lat"] = request.Lat.ToString();

            parameters["lon"] = request.Lon.ToString();

            if (request.Mode != null)
            {
                parameters["mode"] = request.Mode.ToString()?.ToLower();
            }

            if (request.Units != null)
            {
                parameters["units"] = request.Units.ToString()?.ToLower();
            }

            if (request.Lang != null)
            {
                parameters["lang"] = request.Lang;
            }

            return httpClient.GetFromJsonAsync<CurrentWeatherResponse>($"weather?{parameters}");
        }

        public Task<ForecastWeatherResponse?> GetForecast(ForecastWeatherRequest request)
        {
            var parameters = GetParameters();

            parameters["lat"] = request.Lat.ToString();

            parameters["lon"] = request.Lon.ToString();

            if (request.Units != null)
            {
                parameters["units"] = request.Units.ToString()?.ToLower();
            }

            if (request.Mode != null)
            {
                parameters["mode"] = request.Mode.ToString()?.ToLower();
            }

            if (request.Cnt != null)
            {
                parameters["cnt"] = request.Cnt.ToString();
            }

            if (request.Lang != null)
            {
                parameters["lang"] = request.Lang;
            }

            return httpClient.GetFromJsonAsync<ForecastWeatherResponse>($"forecast?{parameters}");
        }
    }
}

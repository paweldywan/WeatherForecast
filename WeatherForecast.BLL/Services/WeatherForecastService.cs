using AutoMapper;
using WeatherForecast.BLL.Interfaces;
using WeatherForecast.BLL.Models;
using WeatherForecast.BLL.Models.WeatherData;

namespace WeatherForecast.BLL.Services
{
    public class WeatherForecastService(IWeatherService weatherService, IMapper mapper) : IWeatherForecastService
    {
        private Task<CurrentWeatherResponse?> GetCurrent(decimal latitude, decimal longitude)
        {
            var request = new CurrentWeatherRequest
            {
                Lat = latitude,
                Lon = longitude
            };

            return weatherService.GetCurrent(request);
        }

        private Task<ForecastWeatherResponse?> GetForecast(decimal latitude, decimal longitude)
        {
            var request = new ForecastWeatherRequest
            {
                Lat = latitude,
                Lon = longitude
            };

            return weatherService.GetForecast(request);
        }

        public async Task<List<WeatherForecastModel>> Get(decimal latitude, decimal longitude)
        {
            var result = new List<WeatherForecastModel>();

            var currentResponse = await GetCurrent(latitude, longitude);

            if (currentResponse != null)
            {
                var current = mapper.Map<WeatherForecastModel>(currentResponse);

                result.Add(current);
            }

            var forecastResponse = await GetForecast(latitude, longitude);

            if (forecastResponse != null)
            {
                var forecast = mapper.Map<List<WeatherForecastModel>>(forecastResponse.List);

                result.AddRange(forecast);
            }

            return result;
        }
    }
}

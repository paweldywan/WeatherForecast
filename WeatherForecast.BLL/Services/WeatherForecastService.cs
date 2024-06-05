using AutoMapper;
using WeatherForecast.BLL.Interfaces;
using WeatherForecast.BLL.Models;
using WeatherForecast.BLL.Models.WeatherData;

namespace WeatherForecast.BLL.Services
{
    public class WeatherForecastService(IWeatherService weatherService, IMapper mapper) : IWeatherForecastService
    {
        public async Task<IEnumerable<WeatherForecastModel>?> Get(decimal latitude, decimal longitude)
        {
            var currentRequest = new CurrentWeatherRequest
            {
                Lat = latitude,
                Lon = longitude
            };

            var currentResponse = await weatherService.GetCurrent(currentRequest);

            var currentWeather = mapper.Map<WeatherForecastModel>(currentResponse);

            var forecastRequest = new ForecastWeatherRequest
            {
                Lat = latitude,
                Lon = longitude
            };

            var forecastResponse = await weatherService.GetForecast(forecastRequest);

            var result = forecastResponse?.List == null ? null : mapper.Map<IEnumerable<WeatherForecastModel>>(forecastResponse.List);

            if (currentWeather != null)
            {
                result ??= [];

                result = result.Prepend(currentWeather);
            }

            return result;
        }
    }
}

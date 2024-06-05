using WeatherForecast.BLL.Interfaces;
using WeatherForecast.BLL.Models;
using WeatherForecast.BLL.Models.WeatherData;

namespace WeatherForecast.BLL.Services
{
    public class WeatherForecastService(IWeatherService weatherService) : IWeatherForecastService
    {
        public async Task<IEnumerable<WeatherForecastModel>?> Get(decimal latitude, decimal longitude)
        {
            var request = new ForecastWeatherRequest
            {
                Lat = latitude,
                Lon = longitude
            };

            var response = await weatherService.GetForecast(request);

            return response?.List.Select(x => new WeatherForecastModel
            {
                Date = DateTimeOffset.FromUnixTimeSeconds(x.Dt).DateTime,
                TemperatureK = x.Main.Temp,
                Summary = x.Weather.First().Description
            });
        }
    }
}

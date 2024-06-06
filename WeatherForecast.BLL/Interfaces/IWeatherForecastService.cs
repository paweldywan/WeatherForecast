using WeatherForecast.BLL.Models;

namespace WeatherForecast.BLL.Interfaces
{
    public interface IWeatherForecastService
    {
        Task<List<WeatherForecastModel>> Get(WeatherForecastRequest request);
    }
}
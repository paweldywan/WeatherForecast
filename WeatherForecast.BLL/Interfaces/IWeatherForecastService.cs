using WeatherForecast.BLL.Models;

namespace WeatherForecast.BLL.Interfaces
{
    public interface IWeatherForecastService
    {
        Task<IEnumerable<WeatherForecastModel>?> Get(decimal latitude, decimal longitude);
    }
}
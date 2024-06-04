using WeatherForecast.BLL.Models.WeatherData;

namespace WeatherForecast.BLL.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherResponse?> Get(WeatherRequest request);
    }
}
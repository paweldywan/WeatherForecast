using WeatherForecast.BLL.Models.WeatherData;

namespace WeatherForecast.BLL.Interfaces
{
    public interface IWeatherService
    {
        Task<CurrentWeatherResponse?> GetCurrent(CurrentWeatherRequest request);
        Task<ForecastWeatherResponse?> GetForecast(ForecastWeatherRequest request);
    }
}
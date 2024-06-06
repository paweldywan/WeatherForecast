using WeatherForecast.BLL.Enums.Weather;
using WeatherForecast.BLL.Interfaces;

namespace WeatherForecast.BLL.Models.WeatherData
{
    public class CurrentWeatherRequest : ICoordinatesRequest
    {
        public float Lat { get; set; }

        public float Lon { get; set; }

        public Mode? Mode { get; set; }

        public Unit? Units { get; set; }

        public string? Lang { get; set; }
    }
}

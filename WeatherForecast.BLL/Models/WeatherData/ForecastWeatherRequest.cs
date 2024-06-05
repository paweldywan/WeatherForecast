using WeatherForecast.BLL.Enums.Weather;

namespace WeatherForecast.BLL.Models.WeatherData
{
    public class ForecastWeatherRequest
    {
        public decimal Lat { get; set; }

        public decimal Lon { get; set; }

        public Unit? Units { get; set; }

        public Mode? Mode { get; set; }

        public int? Cnt { get; set; }

        public string? Lang { get; set; }
    }
}

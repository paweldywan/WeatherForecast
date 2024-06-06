using WeatherForecast.BLL.Enums;

namespace WeatherForecast.BLL.Models
{
    public class WeatherForecastRequest
    {
        public WeatherForecastMode Mode { get; set; }

        public float? Latitude { get; set; }

        public float? Longitude { get; set; }

        public string? ZipCode { get; set; }

        public string? CountryCode { get; set; }

        public string? CityName { get; set; }

        public string? StateCode { get; set; }
    }
}

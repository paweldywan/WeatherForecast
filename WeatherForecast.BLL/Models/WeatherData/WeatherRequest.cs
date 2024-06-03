using System.ComponentModel.DataAnnotations;

namespace WeatherForecast.BLL.Models.WeatherData
{
    public class WeatherRequest
    {
        [Range(-90, 90)]
        public decimal Lat { get; set; }

        [Range(-180, 180)]
        public decimal Lon { get; set; }
    }
}

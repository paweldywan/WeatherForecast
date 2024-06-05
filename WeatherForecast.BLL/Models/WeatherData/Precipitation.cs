using System.Text.Json.Serialization;

namespace WeatherForecast.BLL.Models.WeatherData
{
    public class Precipitation : ThreeHoursPrecipitation
    {
        [JsonPropertyName("1h")]
        public float OneHour { get; set; }
    }
}

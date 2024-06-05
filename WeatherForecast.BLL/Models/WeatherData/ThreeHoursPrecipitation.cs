using System.Text.Json.Serialization;

namespace WeatherForecast.BLL.Models.WeatherData
{
    public class ThreeHoursPrecipitation
    {
        [JsonPropertyName("3h")]
        public float ThreeHours { get; set; }
    }
}

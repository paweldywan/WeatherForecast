using System.Text.Json.Serialization;

namespace WeatherForecast.BLL.Models.WeatherData
{
    public class Main : SimpleMain
    {
        [JsonPropertyName("temp_kf")]
        public float TempKf { get; set; }
    }
}

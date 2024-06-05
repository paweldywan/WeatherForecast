using System.Text.Json.Serialization;

namespace WeatherForecast.BLL.Models.WeatherData
{
    public class List
    {
        public int Dt { get; set; }

        public required Main Main { get; set; }

        public required Weather[] Weather { get; set; }

        public required Clouds Clouds { get; set; }

        public required Wind Wind { get; set; }

        public int Visibility { get; set; }

        public float Pop { get; set; }

        public ThreeHoursPrecipitation? Rain { get; set; }

        public ThreeHoursPrecipitation? Snow { get; set; }

        public required SimpleSys Sys { get; set; }

        [JsonPropertyName("dt_txt")]
        public required string DtTxt { get; set; }
    }
}

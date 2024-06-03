namespace WeatherForecast.BLL.Models.WeatherData
{
    public class Hourly
    {
        public int Dt { get; set; }

        public float Temp { get; set; }

        public float FeelsLike { get; set; }

        public int Pressure { get; set; }

        public int Humidity { get; set; }

        public float DewPoint { get; set; }

        public int Uvi { get; set; }

        public int Clouds { get; set; }

        public int Visibility { get; set; }

        public float WindSpeed { get; set; }

        public int WindDeg { get; set; }

        public float WindGust { get; set; }

        public required Weather1[] Weather { get; set; }

        public float Pop { get; set; }
    }
}

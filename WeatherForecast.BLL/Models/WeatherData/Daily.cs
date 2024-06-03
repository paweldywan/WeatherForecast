namespace WeatherForecast.BLL.Models.WeatherData
{
    public class Daily
    {
        public int Dt { get; set; }

        public int Sunrise { get; set; }

        public int Sunset { get; set; }

        public int Moonrise { get; set; }

        public int Moonset { get; set; }

        public float MoonPhase { get; set; }

        public required string Summary { get; set; }

        public required Temp Temp { get; set; }

        public required FeelsLike FeelsLike { get; set; }

        public int Pressure { get; set; }

        public int Humidity { get; set; }

        public float DewPoint { get; set; }

        public float WindSpeed { get; set; }

        public int WindDeg { get; set; }

        public float WindGust { get; set; }

        public required Weather2[] Weather { get; set; }

        public int Clouds { get; set; }

        public float Pop { get; set; }

        public float Rain { get; set; }

        public float Uvi { get; set; }
    }
}

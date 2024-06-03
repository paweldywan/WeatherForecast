namespace WeatherForecast.BLL.Models.WeatherData
{
    public class Current
    {
        public int Dt { get; set; }

        public int Sunrise { get; set; }

        public int Sunset { get; set; }

        public float Temp { get; set; }

        public float FeelsLike { get; set; }

        public int Pressure { get; set; }

        public int Humidity { get; set; }

        public float DewPoint { get; set; }

        public float Uvi { get; set; }

        public int Clouds { get; set; }

        public int Visibility { get; set; }

        public float WindSpeed { get; set; }

        public int WindDeg { get; set; }

        public float WindGust { get; set; }

        public required Weather[] Weather { get; set; }
    }
}

namespace WeatherForecast.BLL.Models.WeatherData
{
    public class ForecastWeatherResponse
    {
        public required string Cod { get; set; }

        public int Message { get; set; }

        public int Cnt { get; set; }

        public required List[] List { get; set; }

        public required City City { get; set; }
    }
}

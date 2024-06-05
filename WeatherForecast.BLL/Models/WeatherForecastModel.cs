namespace WeatherForecast.BLL.Models
{
    public class WeatherForecastModel
    {
        public DateTime Date { get; set; }

        public float TemperatureK { get; set; }

        public float TemperatureC => TemperatureK - 273.15f;

        public float TemperatureF => TemperatureC * 9 / 5 + 32;

        public required string Summary { get; set; }
    }
}

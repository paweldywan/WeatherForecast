namespace WeatherForecast.BLL.Models.WeatherData
{
    public class Weather2
    {
        public int Id { get; set; }

        public required string Main { get; set; }

        public required string Description { get; set; }

        public required string Icon { get; set; }
    }
}

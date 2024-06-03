namespace WeatherForecast.BLL.Models.WeatherData
{
    public class Alert
    {
        public required string SenderName { get; set; }

        public required string Event { get; set; }

        public int Start { get; set; }

        public int End { get; set; }

        public required string Description { get; set; }

        public required object[] Tags { get; set; }
    }
}

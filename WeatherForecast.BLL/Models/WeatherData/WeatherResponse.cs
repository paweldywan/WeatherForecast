namespace WeatherForecast.BLL.Models.WeatherData
{
    public class WeatherResponse
    {
        public float Lat { get; set; }

        public float Lon { get; set; }

        public required string Timezone { get; set; }

        public int TimezoneOffset { get; set; }

        public required Current Current { get; set; }

        public required Minutely[] Minutely { get; set; }

        public required Hourly[] Hourly { get; set; }

        public required Daily[] Daily { get; set; }

        public required Alert[] Alerts { get; set; }
    }
}

namespace WeatherForecast.BLL.Models.WeatherData
{
    public class City
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required Coord Coord { get; set; }

        public required string Country { get; set; }

        public int Population { get; set; }

        public int Timezone { get; set; }

        public int Sunrise { get; set; }

        public int Sunset { get; set; }
    }
}

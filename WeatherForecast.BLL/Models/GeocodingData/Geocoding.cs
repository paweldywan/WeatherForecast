namespace WeatherForecast.BLL.Models.GeocodingData
{
    public class Geocoding
    {
        public required string Name { get; set; }

        public required LocalNames LocalNames { get; set; }

        public float Lat { get; set; }

        public float Lon { get; set; }

        public required string Country { get; set; }

        public required string State { get; set; }
    }
}

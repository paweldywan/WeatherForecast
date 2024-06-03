namespace WeatherForecast.BLL.Models.GeocodingData
{
    public class GeocodingZipResponse
    {
        public required string Zip { get; set; }

        public required string Name { get; set; }

        public float Lat { get; set; }

        public float Lon { get; set; }

        public required string Country { get; set; }
    }
}

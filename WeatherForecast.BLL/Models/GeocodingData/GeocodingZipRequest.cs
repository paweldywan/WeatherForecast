namespace WeatherForecast.BLL.Models.GeocodingData
{
    public class GeocodingZipRequest
    {
        public required string ZipCode { get; set; }

        public required string CountryCode { get; set; }
    }
}

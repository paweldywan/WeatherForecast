namespace WeatherForecast.BLL.Models.GeocodingData
{
    public class GeocodingLocationRequest
    {
        public required string CityName { get; set; }

        public string? StateCode { get; set; }

        public required string CountryCode { get; set; }

        public int? Limit { get; set; }
    }
}

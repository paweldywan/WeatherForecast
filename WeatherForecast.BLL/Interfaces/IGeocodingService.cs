using WeatherForecast.BLL.Models.GeocodingData;

namespace WeatherForecast.BLL.Interfaces
{
    public interface IGeocodingService
    {
        Task<GeocodingLocationResponse?> Get(GeocodingLocationRequest request);
        Task<GeocodingZipResponse?> Get(GeocodingZipRequest request);
    }
}
using WeatherForecast.BLL.Models.GeocodingData;

namespace WeatherForecast.BLL.Interfaces
{
    public interface IGeocodingService
    {
        Task<Geocoding[]?> GetByLocation(GeocodingLocationRequest request);
        Task<GeocodingZipResponse?> GetByZip(GeocodingZipRequest request);
    }
}
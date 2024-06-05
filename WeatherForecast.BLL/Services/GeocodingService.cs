using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using WeatherForecast.BLL.Interfaces;
using WeatherForecast.BLL.Models.GeocodingData;

namespace WeatherForecast.BLL.Services
{
    public sealed class GeocodingService(HttpClient httpClient, IConfiguration configuration) : Service(configuration), IGeocodingService
    {
        public Task<Geocoding[]?> GetByLocation(GeocodingLocationRequest request)
        {
            var parameters = GetParameters();

            parameters["q"] = new[] { request.CityName, request.StateCode, request.CountryCode }
                .Where(x => x != null)
                .Aggregate((x, y) => $"{x},{y}");

            if (request.Limit != null)
            {
                parameters["limit"] = request.Limit.ToString();
            }

            return httpClient.GetFromJsonAsync<Geocoding[]>($"direct?{parameters}");
        }

        public Task<GeocodingZipResponse?> GetByZip(GeocodingZipRequest request)
        {
            var parameters = GetParameters();

            parameters["zip"] = new[] { request.ZipCode, request.CountryCode }
                .Aggregate((x, y) => $"{x},{y}");

            return httpClient.GetFromJsonAsync<GeocodingZipResponse>($"zip?{parameters}");
        }
    }
}

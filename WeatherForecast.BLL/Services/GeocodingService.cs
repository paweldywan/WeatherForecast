using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Web;
using WeatherForecast.BLL.Interfaces;
using WeatherForecast.BLL.Models.GeocodingData;

namespace WeatherForecast.BLL.Services
{
    public class GeocodingService(HttpClient httpClient, IConfiguration configuration) : Service(configuration), IGeocodingService
    {
        public Task<GeocodingLocationResponse?> Get(GeocodingLocationRequest request)
        {
            var parameters = HttpUtility.ParseQueryString("");

            parameters["q"] = new[] { request.CityName, request.StateCode, request.CountryCode }
                .Where(x => x != null)
                .Aggregate((x, y) => $"{x},{y}");

            parameters["appid"] = ApiKey;

            if (request.Limit != null)
            {
                parameters["limit"] = request.Limit.ToString();
            }

            return httpClient.GetFromJsonAsync<GeocodingLocationResponse>($"?{parameters}");
        }

        public Task<GeocodingZipResponse?> Get(GeocodingZipRequest request)
        {
            var parameters = HttpUtility.ParseQueryString("");

            parameters["zip"] = new[] { request.ZipCode, request.CountryCode }
                .Aggregate((x, y) => $"{x},{y}");

            parameters["appid"] = ApiKey;

            return httpClient.GetFromJsonAsync<GeocodingZipResponse>($"?{parameters}");
        }
    }
}

using AutoMapper;
using WeatherForecast.BLL.Enums;
using WeatherForecast.BLL.Interfaces;
using WeatherForecast.BLL.Models;
using WeatherForecast.BLL.Models.GeocodingData;
using WeatherForecast.BLL.Models.WeatherData;

namespace WeatherForecast.BLL.Services
{
    public class WeatherForecastService(IWeatherService weatherService, IGeocodingService geocodingService, IMapper mapper) : IWeatherForecastService
    {
        private static Task<TOutput?> GetByCoordinates<TRequest, TOutput>(WeatherForecastRequest weatherForecastRequest, Func<TRequest, Task<TOutput?>> func) where TOutput : class where TRequest : ICoordinatesRequest, new()
        {
            if (weatherForecastRequest.Latitude == null || weatherForecastRequest.Longitude == null)
            {
                return Task.FromResult<TOutput?>(null);
            }

            var request = new TRequest
            {
                Lat = weatherForecastRequest.Latitude.Value,
                Lon = weatherForecastRequest.Longitude.Value
            };

            return func(request);
        }

        private async Task<TOutput?> GetByCity<TRequest, TOutput>(WeatherForecastRequest weatherForecastRequest, Func<TRequest, Task<TOutput?>> func) where TOutput : class where TRequest : ICoordinatesRequest, new()
        {
            if (string.IsNullOrWhiteSpace(weatherForecastRequest.CityName) || string.IsNullOrWhiteSpace(weatherForecastRequest.CountryCode))
                return null;

            var geocodingRequest = new GeocodingLocationRequest
            {
                CityName = weatherForecastRequest.CityName,
                StateCode = weatherForecastRequest.StateCode,
                CountryCode = weatherForecastRequest.CountryCode,
                Limit = 1
            };

            var geocodingResponse = await geocodingService.GetByLocation(geocodingRequest);

            if (geocodingResponse == null || geocodingResponse.Length == 0)
                return null;

            var firstResponse = geocodingResponse[0];

            var request = new TRequest
            {
                Lat = firstResponse.Lat,
                Lon = firstResponse.Lon
            };

            return await func(request);
        }

        private async Task<TOutput?> GetByZipCode<TRequest, TOutput>(WeatherForecastRequest weatherForecastRequest, Func<TRequest, Task<TOutput?>> func) where TOutput : class where TRequest : ICoordinatesRequest, new()
        {
            if (string.IsNullOrWhiteSpace(weatherForecastRequest.ZipCode) || string.IsNullOrWhiteSpace(weatherForecastRequest.CountryCode))
                return null;

            var geocodingRequest = new GeocodingZipRequest
            {
                ZipCode = weatherForecastRequest.ZipCode,
                CountryCode = weatherForecastRequest.CountryCode
            };

            var geocodingResponse = await geocodingService.GetByZip(geocodingRequest);

            if (geocodingResponse == null)
                return null;

            var request = new TRequest
            {
                Lat = geocodingResponse.Lat,
                Lon = geocodingResponse.Lon
            };

            return await func(request);
        }

        private Task<TOutput?> Get<TOutput, TRequest>(WeatherForecastRequest weatherForecastRequest, Func<TRequest, Task<TOutput?>> func) where TOutput : class where TRequest : ICoordinatesRequest, new()
        {
            return weatherForecastRequest.Mode switch
            {
                WeatherForecastMode.Location => GetByCoordinates(weatherForecastRequest, func),
                WeatherForecastMode.City => GetByCity(weatherForecastRequest, func),
                WeatherForecastMode.ZipCode => GetByZipCode(weatherForecastRequest, func),
                WeatherForecastMode.Coordinates => GetByCoordinates(weatherForecastRequest, func),
                _ => Task.FromResult<TOutput?>(null)
            };
        }

        private Task<CurrentWeatherResponse?> GetCurrent(WeatherForecastRequest weatherForecastRequest) => Get<CurrentWeatherResponse, CurrentWeatherRequest>(weatherForecastRequest, weatherService.GetCurrent);

        private Task<ForecastWeatherResponse?> GetForecast(WeatherForecastRequest weatherForecastRequest) => Get<ForecastWeatherResponse, ForecastWeatherRequest>(weatherForecastRequest, weatherService.GetForecast);

        public async Task<List<WeatherForecastModel>> Get(WeatherForecastRequest request)
        {
            var result = new List<WeatherForecastModel>();

            var currentResponse = await GetCurrent(request);

            if (currentResponse != null)
            {
                var current = mapper.Map<WeatherForecastModel>(currentResponse);

                result.Add(current);
            }

            var forecastResponse = await GetForecast(request);

            if (forecastResponse != null)
            {
                var forecast = mapper.Map<List<WeatherForecastModel>>(forecastResponse.List);

                result.AddRange(forecast);
            }

            return result;
        }
    }
}

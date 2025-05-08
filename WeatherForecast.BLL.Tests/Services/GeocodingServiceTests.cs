using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http.Json;
using WeatherForecast.BLL.Models.GeocodingData;
using WeatherForecast.BLL.Services;

namespace WeatherForecast.BLL.Tests.Services
{
    public class GeocodingServiceTests
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly GeocodingService _geocodingService;

        public GeocodingServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://api.example.com/")
            };
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(c => c["ApiKey"]).Returns("test-api-key");

            _geocodingService = new GeocodingService(_httpClient, _configurationMock.Object);
        }

        [Fact]
        public async Task GetByLocation_ShouldReturnGeocodingArray_WhenApiCallIsSuccessful()
        {
            // Arrange
            var request = new GeocodingLocationRequest
            {
                CityName = "New York",
                StateCode = "NY",
                CountryCode = "US",
                Limit = 5
            };

            var expectedResponse = new[]
            {
                new Geocoding
                {
                    Name = "New York",
                    Lat = 40.7128f,
                    Lon = -74.0060f,
                    Country = "US",
                    State = "New York"
                }
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(expectedResponse)
                });

            // Act
            var result = await _geocodingService.GetByLocation(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result![0].Name.Should().Be("New York");
            result[0].Lat.Should().Be(40.7128f);
            result[0].Lon.Should().Be(-74.0060f);
            result[0].Country.Should().Be("US");
            result[0].State.Should().Be("New York");
        }

        [Fact]
        public async Task GetByZip_ShouldReturnGeocodingZipResponse_WhenApiCallIsSuccessful()
        {
            // Arrange
            var request = new GeocodingZipRequest
            {
                ZipCode = "10001",
                CountryCode = "US"
            };

            var expectedResponse = new GeocodingZipResponse
            {
                Zip = "10001",
                Name = "New York",
                Lat = 40.7128f,
                Lon = -74.0060f,
                Country = "US"
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(expectedResponse)
                });

            // Act
            var result = await _geocodingService.GetByZip(request);

            // Assert
            result.Should().NotBeNull();
            result!.Zip.Should().Be("10001");
            result.Name.Should().Be("New York");
            result.Lat.Should().Be(40.7128f);
            result.Lon.Should().Be(-74.0060f);
            result.Country.Should().Be("US");
        }

        [Fact]
        public async Task GetByLocation_ShouldThrowError_WhenApiCallFails()
        {
            // Arrange
            var request = new GeocodingLocationRequest
            {
                CityName = "InvalidCity",
                CountryCode = "US"
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            // Act

            Func<Task> act = async () => await _geocodingService.GetByLocation(request);

            // Assert
            var exception = await act.Should().ThrowAsync<HttpRequestException>();

            exception.Which.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetByZip_ShouldThrowError_WhenApiCallFails()
        {
            // Arrange
            var request = new GeocodingZipRequest
            {
                ZipCode = "00000",
                CountryCode = "US"
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            // Act
            Func<Task> act = async () => await _geocodingService.GetByZip(request);

            // Assert
            var exception = await act.Should().ThrowAsync<HttpRequestException>();

            exception.Which.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}

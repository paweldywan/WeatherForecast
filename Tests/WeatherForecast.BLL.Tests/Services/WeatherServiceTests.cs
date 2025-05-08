using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http.Json;
using WeatherForecast.BLL.Enums.Weather;
using WeatherForecast.BLL.Models.WeatherData;
using WeatherForecast.BLL.Services;

namespace WeatherForecast.BLL.Tests.Services
{
    public class WeatherServiceTests
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly WeatherService _weatherService;

        public WeatherServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://api.example.com/")
            };
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(c => c["ApiKey"]).Returns("test-api-key");

            _weatherService = new WeatherService(_httpClient, _configurationMock.Object);
        }

        [Fact]
        public async Task GetCurrent_ShouldReturnCurrentWeatherResponse_WhenApiCallIsSuccessful()
        {
            // Arrange
            var request = new CurrentWeatherRequest
            {
                Lat = 40.7128f,
                Lon = -74.0060f,
                Units = Unit.Metric,
                Lang = "en"
            };

            var expectedResponse = new CurrentWeatherResponse
            {
                Coord = new Coord { Lat = 40.7128f, Lon = -74.0060f },
                Weather = [new Weather { Id = 1, Main = "Clear", Description = "clear sky", Icon = "01d" }],
                Base = "stations",
                Main = new SimpleMain { Temp = 25.0f, FeelsLike = 24.0f, TempMin = 20.0f, TempMax = 30.0f, Pressure = 1013, Humidity = 60 },
                Visibility = 10000,
                Wind = new Wind { Speed = 5.0f, Deg = 180, Gust = 7.0f },
                Clouds = new Clouds { All = 0 },
                Dt = 1625247600,
                Sys = new Sys { Country = "US", Sunrise = 1625212800, Sunset = 1625266800 },
                Timezone = -14400,
                Id = 5128581,
                Name = "New York",
                Cod = 200
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
            var result = await _weatherService.GetCurrent(request);

            // Assert
            result.Should().NotBeNull();
            result!.Name.Should().Be(expectedResponse.Name);
            result.Main.Temp.Should().Be(expectedResponse.Main.Temp);
        }

        [Fact]
        public async Task GetForecast_ShouldReturnForecastWeatherResponse_WhenApiCallIsSuccessful()
        {
            // Arrange
            var request = new ForecastWeatherRequest
            {
                Lat = 40.7128f,
                Lon = -74.0060f,
                Units = Unit.Metric,
                Lang = "en",
                Cnt = 5
            };

            var expectedResponse = new ForecastWeatherResponse
            {
                Cod = "200",
                Message = 0,
                Cnt = 5,
                List =
                [
                    new List
                    {
                        Dt = 1625247600,
                        Main = new Main { Temp = 25.0f, FeelsLike = 24.0f, TempMin = 20.0f, TempMax = 30.0f, Pressure = 1013, Humidity = 60 },
                        Weather = [new Weather { Id = 1, Main = "Clear", Description = "clear sky", Icon = "01d" }],
                        Clouds = new Clouds { All = 0 },
                        Wind = new Wind { Speed = 5.0f, Deg = 180, Gust = 7.0f },
                        Visibility = 10000,
                        Pop = 0.0f,
                        Sys = new SimpleSys { Pod = "d" },
                        DtTxt = "2021-07-02 15:00:00"
                    }
                ],
                City = new City
                {
                    Id = 5128581,
                    Name = "New York",
                    Coord = new Coord { Lat = 40.7128f, Lon = -74.0060f },
                    Country = "US",
                    Population = 8175133,
                    Timezone = -14400,
                    Sunrise = 1625212800,
                    Sunset = 1625266800
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
            var result = await _weatherService.GetForecast(request);

            // Assert
            result.Should().NotBeNull();
            result!.City.Name.Should().Be(expectedResponse.City.Name);
            result.List.Should().HaveCount(expectedResponse.List.Length);
        }
    }
}

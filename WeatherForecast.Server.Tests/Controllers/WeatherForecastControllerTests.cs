using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherForecast.BLL.Enums;
using WeatherForecast.BLL.Interfaces;
using WeatherForecast.BLL.Models;
using WeatherForecast.Server.Controllers;

namespace WeatherForecast.Server.Tests.Controllers
{
    public class WeatherForecastControllerTests
    {
        private readonly Mock<IWeatherForecastService> _weatherForecastServiceMock;
        private readonly WeatherForecastController _controller;

        public WeatherForecastControllerTests()
        {
            _weatherForecastServiceMock = new Mock<IWeatherForecastService>();
            _controller = new WeatherForecastController(_weatherForecastServiceMock.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnOkResult_WithWeatherForecastModels()
        {
            // Arrange
            var request = new WeatherForecastRequest
            {
                Mode = WeatherForecastMode.Coordinates,
                Latitude = 40.7128f,
                Longitude = -74.0060f
            };

            var expectedForecasts = new List<WeatherForecastModel>
            {
                new() {
                    Date = DateTime.Now,
                    TemperatureK = 298.15f,
                    Summary = "Clear"
                },
                new() {
                    Date = DateTime.Now.AddDays(1),
                    TemperatureK = 300.15f,
                    Summary = "Cloudy"
                }
            };

            _weatherForecastServiceMock
                .Setup(service => service.Get(request))
                .ReturnsAsync(expectedForecasts);

            // Act
            var result = await _controller.Get(request);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(expectedForecasts);
        }

        [Fact]
        public async Task Get_ShouldReturnOkResult_WithEmptyList_WhenNoForecastsAreAvailable()
        {
            // Arrange
            var request = new WeatherForecastRequest
            {
                Mode = WeatherForecastMode.City,
                CityName = "InvalidCity",
                CountryCode = "US"
            };

            _weatherForecastServiceMock
                .Setup(service => service.Get(request))
                .ReturnsAsync([]);

            // Act
            var result = await _controller.Get(request);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(new List<WeatherForecastModel>());
        }

        [Fact]
        public async Task Get_ShouldReturnOkResult_WithEmptyList_WhenServiceReturnsEmptyList()
        {
            // Arrange
            var request = new WeatherForecastRequest
            {
                Mode = WeatherForecastMode.ZipCode,
                ZipCode = "00000",
                CountryCode = "US"
            };

            _weatherForecastServiceMock
                .Setup(service => service.Get(request))
                .ReturnsAsync([]);

            // Act
            var result = await _controller.Get(request);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(new List<WeatherForecastModel>());
        }
    }
}


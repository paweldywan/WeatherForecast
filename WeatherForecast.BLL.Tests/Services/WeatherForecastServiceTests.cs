using AutoMapper;
using FluentAssertions;
using Moq;
using WeatherForecast.BLL.Enums;
using WeatherForecast.BLL.Interfaces;
using WeatherForecast.BLL.Models;
using WeatherForecast.BLL.Models.GeocodingData;
using WeatherForecast.BLL.Models.WeatherData;
using WeatherForecast.BLL.Services;

namespace WeatherForecast.BLL.Tests.Services
{
    public class WeatherForecastServiceTests
    {
        private readonly Mock<IWeatherService> _weatherServiceMock;
        private readonly Mock<IGeocodingService> _geocodingServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly WeatherForecastService _weatherForecastService;

        public WeatherForecastServiceTests()
        {
            _weatherServiceMock = new Mock<IWeatherService>();
            _geocodingServiceMock = new Mock<IGeocodingService>();
            _mapperMock = new Mock<IMapper>();

            _weatherForecastService = new WeatherForecastService(
                _weatherServiceMock.Object,
                _geocodingServiceMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task Get_ShouldReturnWeatherForecastModels_WhenModeIsCoordinates()
        {
            // Arrange
            var request = new WeatherForecastRequest
            {
                Mode = WeatherForecastMode.Coordinates,
                Latitude = 40.7128f,
                Longitude = -74.0060f
            };

            var currentWeatherResponse = new CurrentWeatherResponse
            {
                Coord = new Coord { Lat = 40.7128f, Lon = -74.0060f },
                Weather = [new Weather { Main = "Clear", Description = "clear sky", Icon = "01d" }],
                Main = new SimpleMain { Temp = 25.0f },
                Name = "New York",
                Base = "stations",
                Wind = new Wind { Speed = 5.0f, Deg = 180 },
                Clouds = new Clouds { All = 0 },
                Sys = new Sys { Country = "US", Sunrise = 1625212800, Sunset = 1625266800 }
            };

            var forecastWeatherResponse = new ForecastWeatherResponse
            {
                Cod = "200",
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
                },
                List =
                [
                    new List
                    {
                        Dt = 1625247600,
                        Main = new Main { Temp = 25.0f },
                        Weather = [new Weather { Main = "Clear", Description = "clear sky", Icon = "01d" }],
                        Clouds = new Clouds { All = 0 },
                        Wind = new Wind { Speed = 5.0f, Deg = 180 },
                        DtTxt = "2021-07-02 15:00:00",
                        Sys = new SimpleSys { Pod = "d" }
                    }
                ]
            };

            var mappedCurrentWeather = new WeatherForecastModel
            {
                Date = DateTime.Now,
                TemperatureK = 298.15f,
                Summary = "Clear"
            };

            var mappedForecastWeather = new List<WeatherForecastModel>
            {
                new() {
                    Date = DateTime.Now.AddDays(1),
                    TemperatureK = 298.15f,
                    Summary = "Clear"
                }
            };

            _weatherServiceMock
                .Setup(ws => ws.GetCurrent(It.IsAny<CurrentWeatherRequest>()))
                .ReturnsAsync(currentWeatherResponse);

            _weatherServiceMock
                .Setup(ws => ws.GetForecast(It.IsAny<ForecastWeatherRequest>()))
                .ReturnsAsync(forecastWeatherResponse);

            _mapperMock
                .Setup(m => m.Map<WeatherForecastModel>(currentWeatherResponse))
                .Returns(mappedCurrentWeather);

            _mapperMock
                .Setup(m => m.Map<List<WeatherForecastModel>>(forecastWeatherResponse.List))
                .Returns(mappedForecastWeather);

            // Act
            var result = await _weatherForecastService.Get(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].Summary.Should().Be("Clear");
            result[1].Summary.Should().Be("Clear");
        }

        [Fact]
        public async Task Get_ShouldReturnNull_WhenModeIsCityAndGeocodingFails()
        {
            // Arrange
            var request = new WeatherForecastRequest
            {
                Mode = WeatherForecastMode.City,
                CityName = "InvalidCity",
                CountryCode = "US"
            };

            _geocodingServiceMock
                .Setup(gs => gs.GetByLocation(It.IsAny<GeocodingLocationRequest>()))
                .ReturnsAsync((Geocoding[]?)null);

            // Act
            var result = await _weatherForecastService.Get(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task Get_ShouldReturnNull_WhenModeIsZipCodeAndGeocodingFails()
        {
            // Arrange
            var request = new WeatherForecastRequest
            {
                Mode = WeatherForecastMode.ZipCode,
                ZipCode = "00000",
                CountryCode = "US"
            };

            _geocodingServiceMock
                .Setup(gs => gs.GetByZip(It.IsAny<GeocodingZipRequest>()))
                .ReturnsAsync((GeocodingZipResponse?)null);

            // Act
            var result = await _weatherForecastService.Get(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task Get_ShouldReturnEmptyList_WhenNoResponsesAreAvailable()
        {
            // Arrange
            var request = new WeatherForecastRequest
            {
                Mode = WeatherForecastMode.Coordinates,
                Latitude = 40.7128f,
                Longitude = -74.0060f
            };

            _weatherServiceMock
                .Setup(ws => ws.GetCurrent(It.IsAny<CurrentWeatherRequest>()))
                .ReturnsAsync((CurrentWeatherResponse?)null);

            _weatherServiceMock
                .Setup(ws => ws.GetForecast(It.IsAny<ForecastWeatherRequest>()))
                .ReturnsAsync((ForecastWeatherResponse?)null);

            // Act
            var result = await _weatherForecastService.Get(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}

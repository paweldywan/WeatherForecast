using AutoMapper;
using FluentAssertions;
using WeatherForecast.BLL.Models;
using WeatherForecast.BLL.Models.WeatherData;

namespace WeatherForecast.BLL.Tests
{
    public class WeatherForecastMappingProfileTests
    {
        private readonly IMapper _mapper;

        public WeatherForecastMappingProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<WeatherForecastMappingProfile>();
            });

            configuration.AssertConfigurationIsValid(); // Ensures all mappings are valid
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void Should_Map_List_To_WeatherForecastModel_Correctly()
        {
            // Arrange
            var list = new List
            {
                Dt = 1625247600, // Unix timestamp
                Main = new Main { Temp = 298.15f }, // Temperature in Kelvin
                Weather =
                [
                    new Weather { Description = "clear sky", Main = "Clear", Icon = "01d", Id = 800 }
                ],
                Clouds = new Clouds { All = 0 }, // Required member
                Wind = new Wind { Speed = 1.5f, Deg = 350 }, // Required member
                Sys = new SimpleSys { Pod = "d" }, // Required member
                DtTxt = "2021-07-02 12:00:00" // Required member
            };

            // Act
            var result = _mapper.Map<WeatherForecastModel>(list);

            // Assert
            result.Should().NotBeNull();
            result.Date.Should().Be(DateTimeOffset.FromUnixTimeSeconds(list.Dt).DateTime);
            result.TemperatureK.Should().Be(list.Main.Temp);
            result.Summary.Should().Be(list.Weather.First().Description);
        }

        [Fact]
        public void Should_Map_CurrentWeatherResponse_To_WeatherForecastModel_Correctly()
        {
            // Arrange
            var currentWeatherResponse = new CurrentWeatherResponse
            {
                Coord = new Coord { Lon = 0, Lat = 0 }, // Required member
                Weather =
                [
                    new Weather { Id = 800, Main = "Clear", Description = "clear sky", Icon = "01d" }
                ],
                Base = "stations", // Required member
                Main = new SimpleMain { Temp = 298.15f }, // Temperature in Kelvin
                Wind = new Wind { Speed = 1.5f, Deg = 350 }, // Required member
                Clouds = new Clouds { All = 0 }, // Required member
                Sys = new Sys { Country = "US", Sunrise = 1625212800, Sunset = 1625265600 }, // Required member
                Dt = 1625247600, // Unix timestamp
                Name = "Test City", // Required member
                Timezone = 0,
                Id = 12345,
                Cod = 200
            };

            // Act
            var result = _mapper.Map<WeatherForecastModel>(currentWeatherResponse);

            // Assert
            result.Should().NotBeNull();
            result.Date.Should().Be(DateTimeOffset.FromUnixTimeSeconds(currentWeatherResponse.Dt).DateTime);
            result.TemperatureK.Should().Be(currentWeatherResponse.Main.Temp);
            result.Summary.Should().Be(currentWeatherResponse.Weather.First().Description);
        }
    }
}

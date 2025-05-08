using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Collections.Specialized;
using WeatherForecast.BLL.Services;

namespace WeatherForecast.BLL.Tests.Services
{
    public class ServiceTests
    {
        private class TestService(IConfiguration configuration) : Service(configuration)
        {
            public NameValueCollection TestGetParameters() => GetParameters();
        }

        [Fact]
        public void GetParameters_ShouldIncludeApiKey()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["ApiKey"]).Returns("test-api-key");

            var service = new TestService(configurationMock.Object);

            // Act
            var parameters = service.TestGetParameters();

            // Assert
            parameters.Should().NotBeNull();
            parameters["appid"].Should().Be("test-api-key");
        }

        [Fact]
        public void GetParameters_ShouldNotThrowException_WhenApiKeyIsMissing()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["ApiKey"]).Returns((string?)null);

            // Act
            var act = () => new TestService(configurationMock.Object);

            // Assert
            act.Should().NotThrow();
        }
    }
}

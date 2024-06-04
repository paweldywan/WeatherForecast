using Microsoft.Extensions.Configuration;

namespace WeatherForecast.BLL.Services
{
    public abstract class Service(IConfiguration configuration)
    {
        protected readonly string ApiKey = configuration["ApiKey"]!;
    }
}

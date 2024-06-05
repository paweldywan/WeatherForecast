using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;
using System.Web;

namespace WeatherForecast.BLL.Services
{
    public abstract class Service(IConfiguration configuration)
    {
        private readonly string ApiKey = configuration["ApiKey"]!;

        protected NameValueCollection GetParameters()
        {
            var parameters = HttpUtility.ParseQueryString("");

            parameters["appid"] = ApiKey;

            return parameters;
        }
    }
}

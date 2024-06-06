using Microsoft.AspNetCore.Mvc;
using WeatherForecast.BLL.Interfaces;
using WeatherForecast.BLL.Models;

namespace WeatherForecast.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController(IWeatherForecastService weatherForecastService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] WeatherForecastRequest request)
        {
            var result = await weatherForecastService.Get(request);

            return Ok(result);
        }
    }
}

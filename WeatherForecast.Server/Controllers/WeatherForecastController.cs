using Microsoft.AspNetCore.Mvc;
using WeatherForecast.BLL.Interfaces;

namespace WeatherForecast.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController(IWeatherForecastService weatherForecastService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(decimal latitude, decimal longitude)
        {
            var result = await weatherForecastService.Get(latitude, longitude);

            return Ok(result);
        }
    }
}

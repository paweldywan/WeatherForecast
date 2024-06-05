using Microsoft.AspNetCore.Mvc;
using WeatherForecast.BLL.Interfaces;
using WeatherForecast.BLL.Models.WeatherData;

namespace WeatherForecast.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController(IWeatherService weatherService) : ControllerBase
    {
        [HttpGet("Current")]
        public async Task<IActionResult> GetCurrent([FromQuery] CurrentWeatherRequest request)
        {
            var result = await weatherService.GetCurrent(request);

            return Ok(result);
        }

        [HttpGet("Forecast")]
        public async Task<IActionResult> GetForecast([FromQuery] ForecastWeatherRequest request)
        {
            var result = await weatherService.GetForecast(request);

            return Ok(result);
        }
    }
}

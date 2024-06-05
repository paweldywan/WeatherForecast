using Microsoft.AspNetCore.Mvc;
using WeatherForecast.BLL.Interfaces;
using WeatherForecast.BLL.Models.GeocodingData;

namespace WeatherForecast.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeocodingController(IGeocodingService geocodingService) : ControllerBase
    {
        [HttpGet("ByLocation")]
        public async Task<IActionResult> GetByLocation([FromQuery] GeocodingLocationRequest request)
        {
            var result = await geocodingService.GetByLocation(request);

            return Ok(result);
        }

        [HttpGet("ByZip")]
        public async Task<IActionResult> GetByZip([FromQuery] GeocodingZipRequest request)
        {
            var result = await geocodingService.GetByZip(request);

            return Ok(result);
        }
    }
}

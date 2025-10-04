using Microsoft.AspNetCore.Mvc;
using Zephyr.Backend.Contracts.Interfaces;
using Zephyr.Backend.Contracts.Requests;

namespace Zephyr.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController(IWeatherService weatherService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCurrentWeather([FromQuery] GetCurrentWeatherRequest request)
    {
        var result = await weatherService.GetCurrentWeather(request);

        return Ok(result);
    }
}
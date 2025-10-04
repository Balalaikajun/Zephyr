using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zephyr.Backend.Contracts.Requests.Shared.Weather;
using Zephyr.Backend.Models;
using Zephyr.Backend.Services.Interfaces;

namespace Zephyr.Backend.Controllers.Shared;

[ApiController]
[Route("[controller]")]
public class WeatherController(IWeatherService weatherService, IMapper mapper) : BaseController(mapper)
{
    [HttpGet]
    public async Task<IActionResult> GetCurrentWeather([FromQuery] GetCurrentWeatherRequest request)
    {
        var serviceRequest =
            MapToServiceRequest<Services.Requests.GetCurrentWeatherRequest, GetCurrentWeatherRequest>(request);

        return await
            Perform<Weather, Services.Requests.GetCurrentWeatherRequest, Contracts.Responses.Shared.Weather.Weather>(
                serviceRequest, weatherService.GetCurrentWeatherAsync);
    }
}
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zephyr.Backend.Contracts.Requests.Weather;
using Zephyr.Backend.Controllers.Core;
using Zephyr.Backend.Models;
using Zephyr.Backend.Services.Interfaces;

namespace Zephyr.Backend.Controllers;

[ApiController]
[ApiVersion("0.1")]
[Route("v{version:apiVersion}/[controller]")]
public class WeatherController(IWeatherService weatherService, IMapper mapper)
    : BaseController(mapper)
{
    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentWeather([FromQuery] GetCurrentWeatherRequest request)
    {
        var serviceRequest =
            MapToServiceRequest<Services.Requests.Weather.GetCurrentWeatherRequest, GetCurrentWeatherRequest>(request);

        return await
            Perform<Weather, Services.Requests.Weather.GetCurrentWeatherRequest, Contracts.Responses.Weather.Weather>(
                serviceRequest, weatherService.GetCurrentWeatherAsync);
    }
}
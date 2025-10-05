using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zephyr.Backend.Contracts.Requests.Weather;
using Zephyr.Backend.Contracts.Responses.Core;
using Zephyr.Backend.Contracts.Responses.Weather;
using Zephyr.Backend.Controllers.Core;
using Zephyr.Backend.Services.Interfaces;

namespace Zephyr.Backend.Controllers;

[ApiController]
[ApiVersion("0.1")]
[Route("v{version:apiVersion}/[controller]")]
public class WeatherController(IWeatherService weatherService, IMapper mapper)
    : BaseController(mapper)
{
    /// <summary>
    ///     Получить текущую погоду по координатам.
    /// </summary>
    /// <param name="request"> Запрос на получение подсказок </param>
    /// <returns> Текущая погода. </returns>
    [HttpGet("current")]
    [ProducesResponseType(typeof(Weather), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCurrentWeather([FromQuery] GetCurrentWeatherRequest request)
    {
        var serviceRequest =
            MapToServiceRequest<Services.Requests.Weather.GetCurrentWeatherRequest, GetCurrentWeatherRequest>(request);

        return await
            Perform<Models.Weather, Services.Requests.Weather.GetCurrentWeatherRequest, Weather>(
                serviceRequest, weatherService.GetCurrentWeatherAsync);
    }
}
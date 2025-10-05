using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zephyr.Backend.Contracts.Requests.Suggestion;
using Zephyr.Backend.Contracts.Responses.Core;
using Zephyr.Backend.Controllers.Core;
using Zephyr.Backend.Models;
using Zephyr.Backend.Services.Interfaces;

namespace Zephyr.Backend.Controllers;

/// <summary>
/// Контроллер подсказок ввода.
/// </summary>
[ApiController]
[ApiVersion("0.1")]
[Route("v{version:apiVersion}/[controller]")]
public class SuggestionController(
    ISuggestionService suggestionService,
    IMapper mapper) : BaseController(mapper)
{
    /// <summary>
    /// Получить список подсказок для ввода места.
    /// </summary>
    /// <param name="request">Параметры запроса: текст для поиска и количество подсказок.</param>
    /// <returns>Список подсказок.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<Place>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSuggests([FromQuery] GetSuggestRequest request)
    {
        var serviceRequest =
            MapToServiceRequest<Services.Requests.Suggestion.GetSuggestRequest, GetSuggestRequest>(request);

        return await
            Perform<List<Place>, Services.Requests.Suggestion.GetSuggestRequest,
                List<Contracts.Responses.Suggestion.Place>>(serviceRequest,
                suggestionService.GetPlaceSuggestsAsync);
    }
}
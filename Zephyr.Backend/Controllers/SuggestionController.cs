using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zephyr.Backend.Contracts.Requests.Suggestion;
using Zephyr.Backend.Controllers.Core;
using Zephyr.Backend.Models;
using Zephyr.Backend.Services.Interfaces;

namespace Zephyr.Backend.Controllers;

[ApiController]
[ApiVersion("0.1")]
[Route("v{version:apiVersion}/[controller]")]
public class SuggestionController(
    ISuggestionService suggestionService,
    IMapper mapper,
    ILogger<SuggestionController> logger) : BaseController(mapper, logger)
{
    [HttpGet]
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
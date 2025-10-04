using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zephyr.Backend.Contracts.Requests.Shared.Suggestion;
using Zephyr.Backend.Models;
using Zephyr.Backend.Services.Interfaces;

namespace Zephyr.Backend.Controllers.Shared;

[ApiController]
[Route("[controller]")]
public class SuggestionController(
    ISuggestionService suggestionService,
    IMapper mapper,
    ILogger<SuggestionController> logger) : BaseController(mapper, logger)
{
    [HttpGet]
    public async Task<IActionResult> GetSuggests([FromQuery] GetSuggestRequest request)
    {
        var serviceRequest = MapToServiceRequest<Services.Requests.GetSuggestRequest, GetSuggestRequest>(request);

        return await
            Perform<List<Place>, Services.Requests.GetSuggestRequest,
                List<Contracts.Responses.Shared.Suggestion.Place>>(serviceRequest,
                suggestionService.GetPlaceSuggestsAsync);
    }
}
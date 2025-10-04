using Microsoft.AspNetCore.Mvc;
using Zephyr.Backend.Contracts.Interfaces;
using Zephyr.Backend.Contracts.Requests;

namespace Zephyr.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class SuggestionController(ISuggestionService suggestionService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetSuggests([FromQuery] GetSuggestRequest request)
    {
        var result = await suggestionService.GetPlaceSuggests(request);

        return Ok(result);
    }
}
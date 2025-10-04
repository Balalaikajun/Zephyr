using AutoMapper;
using Dadata;
using Zephyr.Backend.Contracts.Interfaces;
using Zephyr.Backend.Contracts.Requests;
using Zephyr.Backend.Models;

namespace Zephyr.Backend.Services;

public class SuggestionService(ISuggestClientAsync suggestionsApiClient, IMapper mapper) : ISuggestionService
{
    public async Task<List<Place>> GetPlaceSuggests(GetSuggestRequest request)
    {
        var result = await suggestionsApiClient.SuggestAddress(request.Query, (int)request.Count);

        return mapper.Map<List<Place>>(result);
    }
}
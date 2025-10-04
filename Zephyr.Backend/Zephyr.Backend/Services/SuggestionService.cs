using AutoMapper;
using Dadata;
using Dadata.Model;
using Zephyr.Backend.Models;
using Zephyr.Backend.Services.Interfaces;
using Zephyr.Backend.Services.Replies.Core;
using Zephyr.Backend.Services.Requests;

namespace Zephyr.Backend.Services;

public class SuggestionService(ISuggestClientAsync suggestionsApiClient, IMapper mapper) : ISuggestionService
{
    public async Task<Reply<List<Place>>> GetPlaceSuggestsAsync(GetSuggestRequest request)
    {
        var response = await suggestionsApiClient.SuggestAddress(
            new SuggestAddressRequest(request.Query, (int)request.Count)
            {
                from_bound = new AddressBound("city"),
                to_bound = new AddressBound("house")
            });

        var result = mapper.Map<List<Place>>(response);

        return Reply<List<Place>>.Success(result);
    }
}
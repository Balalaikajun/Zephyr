using Zephyr.Backend.Contracts.Requests;
using Zephyr.Backend.Models;

namespace Zephyr.Backend.Contracts.Interfaces;

public interface ISuggestionService
{
    Task<List<Place>> GetPlaceSuggests(GetSuggestRequest request);
}
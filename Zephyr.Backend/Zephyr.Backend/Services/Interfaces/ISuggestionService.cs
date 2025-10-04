using Zephyr.Backend.Models;
using Zephyr.Backend.Services.Replies.Core;
using Zephyr.Backend.Services.Requests;

namespace Zephyr.Backend.Services.Interfaces;

public interface ISuggestionService
{
    Task<Reply<List<Place>>> GetPlaceSuggestsAsync(GetSuggestRequest request);
}
namespace Zephyr.Backend.Contracts.Requests.Shared.Suggestion;

public record GetSuggestRequest
{
    public required string Query { get; init; }
    public required uint Count { get; init; } = 5;
}
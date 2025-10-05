using System.ComponentModel;

namespace Zephyr.Backend.Contracts.Requests.Suggestion;

public record GetSuggestRequest
{
    public required string Query { get; init; }

    [DefaultValue(5)] public required uint Count { get; init; } = 5;
}
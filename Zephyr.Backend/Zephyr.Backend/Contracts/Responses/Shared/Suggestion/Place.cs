namespace Zephyr.Backend.Contracts.Responses.Shared.Suggestion;

public record Place
{
    public required string Name { get; init; }
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }
}
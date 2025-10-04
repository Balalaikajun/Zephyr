namespace Zephyr.Backend.Models;

public record Place
{
    public required string Name { get; init; }
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }
}
namespace Zephyr.Backend.Models;

public record Weather
{
    public required double Temperature { get; init; }
    public required int Humidity { get; init; }
    public required int Pressure { get; init; }
}
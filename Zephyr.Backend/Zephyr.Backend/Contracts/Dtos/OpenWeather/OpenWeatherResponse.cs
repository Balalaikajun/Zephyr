namespace Zephyr.Backend.Contracts.Dtos.OpenWeather;

public record OpenWeatherResponse
{
    public MainInfo Main { get; init; } = null!;
}
namespace Zephyr.Backend.Utils.Dtos.OpenWeather;

public record OpenWeatherResponse
{
    public MainInfo Main { get; init; } = null!;
}
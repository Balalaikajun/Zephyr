namespace Zephyr.Backend.Utils.Weather.Dtos.OpenWeather;

public record OpenWeatherResponse
{
    public MainInfo Main { get; init; } = null!;
}
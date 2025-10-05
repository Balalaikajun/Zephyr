using Zephyr.Backend.Utils.Weather.Enums;

namespace Zephyr.Backend.Infrastructure;

public record Settings
{
    public required Dictionary<WeatherProvider, string> WeatherApiBaseUrls { get; init; }
}
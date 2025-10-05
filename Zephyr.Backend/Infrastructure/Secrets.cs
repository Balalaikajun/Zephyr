using Zephyr.Backend.Utils.Weather.Enums;

namespace Zephyr.Backend.Infrastructure;

public record Secrets
{
    public required Dictionary<WeatherProvider, string> WeatherApiKeys { get; init; }
    public required string DadataToken { get; init; }
}
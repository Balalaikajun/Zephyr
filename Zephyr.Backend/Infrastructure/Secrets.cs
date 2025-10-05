using Zephyr.Backend.Utils.Weather.Enums;

namespace Zephyr.Backend.Infrastructure;

/// <summary>
///     Секреты приложения
/// </summary>
public record Secrets
{
    /// <summary>
    ///     Ключи для Api погоды
    /// </summary>
    public required Dictionary<WeatherProvider, string> WeatherApiKeys { get; init; }

    /// <summary>
    ///     Токен доступа Dadata
    /// </summary>
    public required string DadataToken { get; init; }
}
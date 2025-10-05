using Zephyr.Backend.Utils.Weather.Enums;

namespace Zephyr.Backend.Infrastructure;

/// <summary>
/// Настройки приложения
/// </summary>
public record Settings
{
    /// <summary>
    /// Базовые пути до Api погоды
    /// </summary>
    public required Dictionary<WeatherProvider, string> WeatherApiBaseUrls { get; init; }

    /// <summary>
    /// Таймаут для провайдеров погоды
    /// </summary>
    public required int ApiClientsTimeoutFromSeconds { get; init; } = 10;
}
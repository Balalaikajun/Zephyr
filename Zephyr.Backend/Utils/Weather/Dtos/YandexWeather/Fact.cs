using System.Text.Json.Serialization;

namespace Zephyr.Backend.Utils.Weather.Dtos.YandexWeather;

/// <summary>
///     Фактические погодные данные от Яндекс.Погоды.
/// </summary>
public record Fact
{
    /// <summary>
    ///     Температура воздуха (°C).
    /// </summary>
    [JsonPropertyName("temp")]
    public required int Temperature { get; init; }

    /// <summary>
    ///     Влажность воздуха (%).
    /// </summary>
    [JsonPropertyName("humidity")]
    public required int Humidity { get; init; }

    /// <summary>
    ///     Атмосферное давление (мм. рт. ст.).
    /// </summary>
    [JsonPropertyName("pressure_pa")]
    public required int Pressure { get; init; }
}
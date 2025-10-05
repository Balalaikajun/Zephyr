using System.Text.Json.Serialization;

namespace Zephyr.Backend.Utils.Weather.Dtos.OpenWeather;

/// <summary>
/// Основные метеопараметры из OpenWeather.
/// </summary>
public record MainInfo
{
    /// <summary>
    /// Температура воздуха (°C, если в настройках API указан units=metric).
    /// </summary>
    [JsonPropertyName("temp")] 
    public double Temperature { get; init; }

    /// <summary>
    /// Влажность воздуха (%).
    /// </summary>
    [JsonPropertyName("humidity")] 
    public int Humidity { get; init; }

    /// <summary>
    /// Атмосферное давление (мм. рт. ст.).
    /// </summary>
    [JsonPropertyName("pressure")] 
    public int Pressure { get; init; }
}
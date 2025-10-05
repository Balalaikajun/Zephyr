using System.Text.Json.Serialization;

namespace Zephyr.Backend.Utils.Weather.Dtos.OpenWeather;

public record MainInfo
{
    [JsonPropertyName("temp")] public double Temperature { get; init; }

    [JsonPropertyName("humidity")] public int Humidity { get; init; }

    [JsonPropertyName("pressure")] public int Pressure { get; init; }
}
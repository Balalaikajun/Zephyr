using System.Text.Json.Serialization;

namespace Zephyr.Backend.Utils.Dtos.YandexWeather;

public record Fact
{
    [JsonPropertyName("temp")] public required int Temperature { get; init; }

    [JsonPropertyName("humidity")] public required int Humidity { get; init; }

    [JsonPropertyName("pressure_pa")] public required int Pressure { get; init; }
}
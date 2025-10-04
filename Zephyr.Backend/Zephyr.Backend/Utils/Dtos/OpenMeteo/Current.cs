using System.Text.Json.Serialization;

namespace Zephyr.Backend.Utils.Dtos.OpenMeteo;

public record Current
{
    [JsonPropertyName("time")] public DateTime Time { get; init; }

    [JsonPropertyName("interval")] public int Interval { get; init; }

    [JsonPropertyName("temperature_2m")] public double Temperature { get; init; }

    [JsonPropertyName("relative_humidity_2m")]
    public int Humidity { get; init; }

    [JsonPropertyName("surface_pressure")] public double Pressure { get; init; }
}
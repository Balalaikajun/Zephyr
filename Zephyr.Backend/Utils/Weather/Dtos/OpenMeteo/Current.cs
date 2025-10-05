using System.Text.Json.Serialization;

namespace Zephyr.Backend.Utils.Weather.Dtos.OpenMeteo;

public record Current
{
    [JsonPropertyName("temperature_2m")] public double Temperature { get; init; }

    [JsonPropertyName("relative_humidity_2m")]
    public int Humidity { get; init; }

    [JsonPropertyName("surface_pressure")] public double Pressure { get; init; }
}
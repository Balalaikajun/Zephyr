using System.Text.Json.Serialization;

namespace Zephyr.Backend.Utils.Dtos.OpenMeteo;

public record OpenMeteoResponse
{
    [JsonPropertyName("latitude")] public double Latitude { get; init; }

    [JsonPropertyName("longitude")] public double Longitude { get; init; }

    [JsonPropertyName("current")] public Current Current { get; init; }
}
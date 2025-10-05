using System.Text.Json.Serialization;

namespace Zephyr.Backend.Utils.Weather.Dtos.OpenMeteo;

public record OpenMeteoResponse
{
    [JsonPropertyName("current")] public required Current Current { get; init; }
}
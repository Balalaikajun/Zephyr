using System.Text.Json.Serialization;

namespace Zephyr.Backend.Utils.Dtos.OpenMeteo;

public record OpenMeteoResponse
{
    [JsonPropertyName("current")] public Current Current { get; init; }
}
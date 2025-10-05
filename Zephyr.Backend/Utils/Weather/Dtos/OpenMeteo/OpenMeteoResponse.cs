using System.Text.Json.Serialization;

namespace Zephyr.Backend.Utils.Weather.Dtos.OpenMeteo;

/// <summary>
///     Ответ OpenMeteo API с текущими погодными данными.
/// </summary>
public record OpenMeteoResponse
{
    /// <summary>
    ///     Текущие метеопараметры.
    /// </summary>
    [JsonPropertyName("current")]
    public required Current Current { get; init; }
}
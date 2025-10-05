using System.Text.Json.Serialization;

namespace Zephyr.Backend.Utils.Weather.Dtos.OpenMeteo;

public record Current
{
    /// <summary>
    ///     Температура воздуха на высоте 2м (°C).
    /// </summary>
    [JsonPropertyName("temperature_2m")]
    public double Temperature { get; init; }

    /// <summary>
    ///     Относительная влажность воздуха на высоте 2м (%).
    /// </summary>
    [JsonPropertyName("relative_humidity_2m")]
    public int Humidity { get; init; }

    /// <summary>
    ///     Атмосферное давление у поверхности (мм рт. ст.).
    /// </summary>
    [JsonPropertyName("surface_pressure")]
    public double Pressure { get; init; }
}
namespace Zephyr.Backend.Models;

/// <summary>
/// Данные о погоде
/// </summary>
public record Weather
{
    /// <summary>
    /// Температура
    /// </summary>
    public required double Temperature { get; init; }

    /// <summary>
    /// Влажность
    /// </summary>
    public required int Humidity { get; init; }

    /// <summary>
    /// Давление
    /// </summary>
    public required int Pressure { get; init; }
}
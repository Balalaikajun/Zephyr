namespace Zephyr.Backend.Utils.Weather.Dtos.OpenWeather;

/// <summary>
/// Ответ API OpenWeather с основными погодными параметрами.
/// </summary>
public record OpenWeatherResponse
{
    /// <summary>
    /// Основная информация о погоде.
    /// </summary>
    public MainInfo Main { get; init; } = null!;
}
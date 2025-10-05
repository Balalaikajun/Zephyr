namespace Zephyr.Backend.Utils.Weather.Dtos.YandexWeather;

/// <summary>
///     Ответ API Яндекс.Погоды с текущими погодными данными.
/// </summary>
public class YandexWeatherResponse
{
    /// <summary>
    ///     Фактические (текущие) метеопараметры.
    /// </summary>
    public required Fact Fact { get; init; }
}
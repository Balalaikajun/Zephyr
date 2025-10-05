namespace Zephyr.Backend.Utils.Weather.Enums;

/// <summary>
///     Поддерживаемые провайдеры погоды.
/// </summary>
public enum WeatherProvider
{
    /// <summary>
    ///     <see href="https://openweathermap.org/">OpenWeather</see>.
    /// </summary>
    OpenWeather = 1,

    /// <summary>
    ///     <see href="https://open-meteo.com/">OpenMeteo</see>.
    /// </summary>
    OpenMeteo = 2,

    /// <summary>
    ///     <see href="https://yandex.ru/dev/weather/">Яндекс.Погода</see>
    /// </summary>
    YandexWeather = 3
}
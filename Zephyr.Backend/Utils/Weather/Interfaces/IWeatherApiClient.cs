using Zephyr.Backend.Services.Replies.Core;
using Zephyr.Backend.Utils.Weather.Enums;

namespace Zephyr.Backend.Utils.Weather.Interfaces;

/// <summary>
/// Клиент провайдера погоды.
/// </summary>
public interface IWeatherApiClient
{
    /// <summary>
    /// Провайдер погоды, которого реализует клиент.
    /// </summary>
    WeatherProvider WeatherProvider { get; }

    /// <summary>
    /// Получить текущую погоду по заданным координатам.
    /// </summary>
    /// <param name="latitude">Географическая широта.</param>
    /// <param name="longitude">Географическая долгота.</param>
    /// <returns>Текущая погода. </returns>
    Task<Reply<Models.Weather>> GetCurrentWeather(double latitude, double longitude);
}
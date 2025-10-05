using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Zephyr.Backend.Utils.Weather.Enums;

namespace Zephyr.Backend.Contracts.Requests.Weather;

/// <summary>
/// Запрос для получения текущей погоды по координатам.
/// </summary>
public record GetCurrentWeatherRequest
{
    /// <summary>
    /// Широта
    /// </summary>
    [Range(-90, 90)]
    [DefaultValue(55.7558)]
    public required double Latitude { get; init; }

    /// <summary>
    /// Долгота
    /// </summary>
    [Range(-180, 180)]
    [DefaultValue(37.6173)]
    public required double Longitude { get; init; }

    /// <summary>
    /// Провайдер погоды, который будет использоваться для получения данных.
    /// </summary>
    [DefaultValue(WeatherProvider.OpenWeather)]
    public WeatherProvider WeatherProvider { get; init; } = WeatherProvider.OpenWeather;
}
using Zephyr.Backend.Utils.Weather.Enums;

namespace Zephyr.Backend.Services.Requests.Weather;

/// <summary>
/// Запрос для получения текущей погоды по координатам.
/// </summary>
public record GetCurrentWeatherRequest
{
    /// <summary>
    /// Широта
    /// </summary>
    public required double Latitude { get; init; }
    
    /// <summary>
    /// Долгота
    /// </summary>
    public required double Longitude { get; init; }
    
    /// <summary>
    /// Провайдер погоды, который будет использоваться для получения данных.
    /// </summary>
    public WeatherProvider WeatherProvider { get; init; }
}
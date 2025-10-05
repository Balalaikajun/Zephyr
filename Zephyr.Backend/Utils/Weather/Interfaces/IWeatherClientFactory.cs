using Zephyr.Backend.Utils.Weather.Enums;

namespace Zephyr.Backend.Utils.Weather.Interfaces;

/// <summary>
///     Фабрика для создания <see cref="IWeatherApiClient" />
/// </summary>
public interface IWeatherClientFactory
{
    /// <summary>
    ///     Создать <see cref="IWeatherApiClient" /> указанного провайдера
    /// </summary>
    /// <param name="provider">Провайдер погоды</param>
    /// <returns>Клиент провайдера погоды</returns>
    IWeatherApiClient Create(WeatherProvider provider);
}
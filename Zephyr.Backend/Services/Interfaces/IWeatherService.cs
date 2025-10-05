using Zephyr.Backend.Models;
using Zephyr.Backend.Services.Replies.Core;
using Zephyr.Backend.Services.Requests.Weather;

namespace Zephyr.Backend.Services.Interfaces;

/// <summary>
/// Сервис получения данных погоды
/// </summary>
public interface IWeatherService
{
    /// <summary>
    /// Получить текущие данные о погоде
    /// </summary>
    /// <param name="request">Запрос на получение данных о погоде</param>
    /// <returns>Текущие данные о погоде</returns>
    Task<Reply<Weather>> GetCurrentWeatherAsync(GetCurrentWeatherRequest request);
}
using Zephyr.Backend.Contracts.Requests;
using Zephyr.Backend.Models;

namespace Zephyr.Backend.Contracts.Interfaces;

public interface IWeatherService
{
    Task<Weather> GetCurrentWeather(GetCurrentWeatherRequest request);
}
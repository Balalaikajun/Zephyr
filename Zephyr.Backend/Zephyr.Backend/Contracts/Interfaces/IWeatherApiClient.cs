using Zephyr.Backend.Infrastructure;
using Zephyr.Backend.Infrastructure.Enums;
using Zephyr.Backend.Models;

namespace Zephyr.Backend.Contracts.Interfaces;

public interface IWeatherApiClient
{
    static WeatherProvider WeatherProvider { get; }
    Task<Weather> GetWeather(double latitude, double longitude);
}
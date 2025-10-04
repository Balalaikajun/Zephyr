using Zephyr.Backend.Models;

namespace Zephyr.Backend.Utils.Interfaces;

public interface IWeatherApiClient
{
    static WeatherProvider WeatherProvider { get; }
    Task<Weather> GetWeather(double latitude, double longitude);
}
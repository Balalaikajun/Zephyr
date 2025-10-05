using Zephyr.Backend.Services.Replies.Core;
using Zephyr.Backend.Utils.Weather.Enums;

namespace Zephyr.Backend.Utils.Weather.Interfaces;

public interface IWeatherApiClient
{
    WeatherProvider WeatherProvider { get; }
    Task<Reply<Models.Weather>> GetWeather(double latitude, double longitude);
}
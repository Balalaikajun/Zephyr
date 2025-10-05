using Zephyr.Backend.Models;
using Zephyr.Backend.Services.Replies.Core;
using Zephyr.Backend.Utils.Enums;

namespace Zephyr.Backend.Utils.Interfaces;

public interface IWeatherApiClient
{
    WeatherProvider WeatherProvider { get; }
    Task<Reply<Weather>> GetWeather(double latitude, double longitude);
}
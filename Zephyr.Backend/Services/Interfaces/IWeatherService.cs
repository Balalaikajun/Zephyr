using Zephyr.Backend.Models;
using Zephyr.Backend.Services.Replies.Core;
using Zephyr.Backend.Services.Requests.Weather;

namespace Zephyr.Backend.Services.Interfaces;

public interface IWeatherService
{
    Task<Reply<Weather>> GetCurrentWeatherAsync(GetCurrentWeatherRequest request);
}
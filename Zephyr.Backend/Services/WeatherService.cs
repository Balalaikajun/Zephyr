using Zephyr.Backend.Models;
using Zephyr.Backend.Services.Interfaces;
using Zephyr.Backend.Services.Replies.Core;
using Zephyr.Backend.Services.Requests.Weather;
using Zephyr.Backend.Utils.Weather.Interfaces;

namespace Zephyr.Backend.Services;

/// <inheritdoc/>
public class WeatherService(IWeatherClientFactory weatherClientFactory) : IWeatherService
{
    /// <inheritdoc/>
    public async Task<Reply<Weather>> GetCurrentWeatherAsync(GetCurrentWeatherRequest request)
    {
        var client = weatherClientFactory.Create(request.WeatherProvider);

        return await client.GetCurrentWeather(request.Latitude, request.Longitude);
    }
}
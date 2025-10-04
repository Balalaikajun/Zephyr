using Zephyr.Backend.Models;
using Zephyr.Backend.Services.Interfaces;
using Zephyr.Backend.Services.Replies.Core;
using Zephyr.Backend.Services.Requests;
using Zephyr.Backend.Utils.Interfaces;

namespace Zephyr.Backend.Services;

public class WeatherService(IWeatherClientFactory weatherClientFactory) : IWeatherService
{
    public async Task<Reply<Weather>> GetCurrentWeatherAsync(GetCurrentWeatherRequest request)
    {
        var client = weatherClientFactory.Create(request.WeatherProvider);

        return await client.GetWeather(request.Latitude, request.Longitude);
    }
}
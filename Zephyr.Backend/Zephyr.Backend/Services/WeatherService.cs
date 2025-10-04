using Zephyr.Backend.Models;
using Zephyr.Backend.Services.Interfaces;
using Zephyr.Backend.Services.Replies.Core;
using Zephyr.Backend.Services.Requests;
using Zephyr.Backend.Utils.Interfaces;

namespace Zephyr.Backend.Services;

public class WeatherService(IWeatherApiClient client) : IWeatherService
{
    public async Task<Reply<Weather>> GetCurrentWeatherAsync(GetCurrentWeatherRequest request)
    {
        var result = await client.GetWeather(request.Latitude, request.Longitude);

        return Reply<Weather>.Success(result);
    }
}
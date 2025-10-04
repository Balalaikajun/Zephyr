using Zephyr.Backend.Contracts.Interfaces;
using Zephyr.Backend.Contracts.Requests;
using Zephyr.Backend.Models;

namespace Zephyr.Backend.Services;

public class WeatherService(IWeatherApiClient client) : IWeatherService
{
    public async Task<Weather> GetCurrentWeather(GetCurrentWeatherRequest request)
    {
        return await client.GetWeather(request.Latitude, request.Longitude);
    }
}
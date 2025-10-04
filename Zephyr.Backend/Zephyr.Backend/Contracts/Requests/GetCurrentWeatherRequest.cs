using Zephyr.Backend.Infrastructure;
using Zephyr.Backend.Infrastructure.Enums;

namespace Zephyr.Backend.Contracts.Requests;

public record GetCurrentWeatherRequest
{
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }
    public WeatherProvider WeatherProvider { get; init; } = WeatherProvider.OpenWeather;
}
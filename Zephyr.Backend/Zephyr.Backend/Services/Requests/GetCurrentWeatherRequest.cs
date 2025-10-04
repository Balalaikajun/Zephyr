using Zephyr.Backend.Utils.Enums;

namespace Zephyr.Backend.Services.Requests;

public record GetCurrentWeatherRequest
{
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }
    public WeatherProvider WeatherProvider { get; init; } = WeatherProvider.OpenWeather;
}
using Zephyr.Backend.Utils.Weather.Enums;

namespace Zephyr.Backend.Services.Requests.Weather;

public record GetCurrentWeatherRequest
{
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }
    public WeatherProvider WeatherProvider { get; init; } = WeatherProvider.OpenWeather;
}
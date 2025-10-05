using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Zephyr.Backend.Utils.Weather.Enums;

namespace Zephyr.Backend.Contracts.Requests.Weather;

public record GetCurrentWeatherRequest
{
    [Range(-90, 90)]
    [DefaultValue(55.7558)]
    public required double Latitude { get; init; }

    [Range(-180, 180)]
    [DefaultValue(37.6173)]
    public required double Longitude { get; init; }

    [DefaultValue(WeatherProvider.OpenWeather)]
    public WeatherProvider WeatherProvider { get; init; } = WeatherProvider.OpenWeather;
}
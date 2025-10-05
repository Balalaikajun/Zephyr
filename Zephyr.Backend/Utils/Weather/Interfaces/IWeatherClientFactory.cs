using Zephyr.Backend.Utils.Weather.Enums;

namespace Zephyr.Backend.Utils.Weather.Interfaces;

public interface IWeatherClientFactory
{
    IWeatherApiClient Create(WeatherProvider provider);
}
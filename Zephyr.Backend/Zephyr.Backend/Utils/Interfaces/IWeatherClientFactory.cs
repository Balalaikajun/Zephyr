using Zephyr.Backend.Utils.Enums;

namespace Zephyr.Backend.Utils.Interfaces;

public interface IWeatherClientFactory
{
    IWeatherApiClient Create(WeatherProvider provider);
}
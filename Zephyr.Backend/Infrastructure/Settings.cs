using Zephyr.Backend.Utils.Weather.Enums;

namespace Zephyr.Backend.Infrastructure;

public class Settings
{
    public required Dictionary<WeatherProvider, string> WeatherApiBaseUrls { get; set; }
}
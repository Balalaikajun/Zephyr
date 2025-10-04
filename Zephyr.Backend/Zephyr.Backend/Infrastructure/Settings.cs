using Zephyr.Backend.Utils.Enums;

namespace Zephyr.Backend.Infrastructure;

public class Settings
{
    public required Dictionary<WeatherProvider, string> WeatherApiBaseUrls { get; set; }
}
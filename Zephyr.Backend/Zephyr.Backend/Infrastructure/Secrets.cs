using Zephyr.Backend.Utils.Interfaces;

namespace Zephyr.Backend.Infrastructure;

public class Secrets
{
    public required Dictionary<WeatherProvider, string> WeatherApiKeys { get; set; }
    public required string DadataToken { get; set; }
}
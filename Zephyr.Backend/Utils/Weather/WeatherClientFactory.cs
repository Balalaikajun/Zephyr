using Zephyr.Backend.Utils.Weather.ApiClients;
using Zephyr.Backend.Utils.Weather.Enums;
using Zephyr.Backend.Utils.Weather.Interfaces;

namespace Zephyr.Backend.Utils.Weather;

/// <inheritdoc/>
public class WeatherClientFactory(IServiceProvider sp) : IWeatherClientFactory
{
    private readonly Dictionary<WeatherProvider, Type> _map = new()
    {
        { WeatherProvider.OpenWeather, typeof(OpenWeatherClient) },
        { WeatherProvider.OpenMeteo, typeof(OpenMeteoClient) },
        { WeatherProvider.YandexWeather, typeof(YandexWeatherClient) }
    };

    /// <inheritdoc/>
    public IWeatherApiClient Create(WeatherProvider provider)
    {
        if (!_map.TryGetValue(provider, out var type))
            throw new NotSupportedException($"Клиент для {provider} не зарегистрирован");

        return (IWeatherApiClient)sp.GetRequiredService(type);
    }
}
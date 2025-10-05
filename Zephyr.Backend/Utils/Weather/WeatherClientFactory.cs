using Zephyr.Backend.Utils.Weather.Enums;
using Zephyr.Backend.Utils.Weather.Interfaces;

namespace Zephyr.Backend.Utils.Weather;

/// <inheritdoc/>
public class WeatherClientFactory(IEnumerable<IWeatherApiClient> weatherApiClients) : IWeatherClientFactory
{
    /// <inheritdoc/>
    public IWeatherApiClient Create(WeatherProvider provider)
    {
        var client = weatherApiClients.FirstOrDefault(x => x.WeatherProvider == provider);

        if (client == null) throw new NotSupportedException($"Клиент для {provider} не зарегистрирован");

        return client;
    }
}
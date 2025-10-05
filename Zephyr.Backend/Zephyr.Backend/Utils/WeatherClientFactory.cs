using Zephyr.Backend.Utils.ApiClients;
using Zephyr.Backend.Utils.Enums;
using Zephyr.Backend.Utils.Interfaces;

namespace Zephyr.Backend.Utils;

public class WeatherClientFactory(IEnumerable<IWeatherApiClient> weatherApiClients) : IWeatherClientFactory
{
    public IWeatherApiClient Create(WeatherProvider provider)
    {
        var client = weatherApiClients.FirstOrDefault(x => x.WeatherProvider == provider);

        if (client == null)
        {
            throw new NotSupportedException($"Клиент для {provider} не зарегистрирован");
        }

        return client;
    }
}
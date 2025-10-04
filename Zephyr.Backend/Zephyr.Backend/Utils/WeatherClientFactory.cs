using Zephyr.Backend.Utils.ApiClients;
using Zephyr.Backend.Utils.Enums;
using Zephyr.Backend.Utils.Interfaces;

namespace Zephyr.Backend.Utils;

public class WeatherClientFactory(IServiceProvider serviceProvider) : IWeatherClientFactory
{
    public IWeatherApiClient Create(WeatherProvider provider)
    {
        return provider switch
        {
            WeatherProvider.OpenWeather => serviceProvider.GetRequiredService<OpenWeatherClient>(),
            WeatherProvider.OpenMeteo => serviceProvider.GetRequiredService<OpenMeteoClient>(),
            WeatherProvider.YandexWeather => serviceProvider.GetRequiredService<YandexWeatherClient>(),
            _ => throw new NotSupportedException($"Клиент для {provider} не зарегистрирован")
        };
    }
}
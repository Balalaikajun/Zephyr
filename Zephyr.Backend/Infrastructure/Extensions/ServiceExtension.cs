using System.Reflection;
using Zephyr.Backend.Utils.Weather;
using Zephyr.Backend.Utils.Weather.ApiClients;
using Zephyr.Backend.Utils.Weather.Enums;
using Zephyr.Backend.Utils.Weather.Interfaces;

namespace Zephyr.Backend.Infrastructure.Extensions;

/// <summary>
/// Расширение для регестрации провайдеров погоды
/// </summary>
public static class ServiceExtension
{
    /// <summary>
    /// Регистрирует реализации <see cref="IWeatherApiClient"/>
    /// и фабрику клиентов погоды <see cref="IWeatherClientFactory"/>.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации.</param>
    /// <param name="secrets">Секреты для конфигурации клиентов</param>
    /// <param name="settings">Настройки для конфигурации клиентов</param>
    public static void AddWeatherApiClients(this IServiceCollection services, Settings settings, Secrets secrets)
    {
        services.AddHttpClient<OpenWeatherClient>(client =>
        {
            client.BaseAddress = new Uri(settings.WeatherApiBaseUrls[WeatherProvider.OpenWeather]);
            client.Timeout = TimeSpan.FromSeconds(settings.ApiClientsTimeoutFromSeconds);
        });

        services.AddHttpClient<OpenMeteoClient>(client =>
        {
            client.BaseAddress = new Uri(settings.WeatherApiBaseUrls[WeatherProvider.OpenMeteo]);
            client.Timeout = TimeSpan.FromSeconds(settings.ApiClientsTimeoutFromSeconds);
        });

        services.AddHttpClient<YandexWeatherClient>(client =>
        {
            client.BaseAddress = new Uri(settings.WeatherApiBaseUrls[WeatherProvider.YandexWeather]);
            client.DefaultRequestHeaders.Add("X-Yandex-Weather-Key",
                secrets.WeatherApiKeys[WeatherProvider.YandexWeather]);
            client.Timeout = TimeSpan.FromSeconds(settings.ApiClientsTimeoutFromSeconds);
        });

        services.AddScoped<IWeatherClientFactory, WeatherClientFactory>();
    }
}
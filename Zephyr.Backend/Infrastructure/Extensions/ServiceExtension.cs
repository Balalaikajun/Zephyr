using System.Reflection;
using Zephyr.Backend.Utils.Weather;
using Zephyr.Backend.Utils.Weather.Interfaces;

namespace Zephyr.Backend.Infrastructure.Extensions;

/// <summary>
/// Расширение для регестрации провайдеров погоды
/// </summary>
public static class ServiceExtension
{
    /// <summary>
    /// Регистрирует все реализации <see cref="IWeatherApiClient"/> из указанной сборки 
    /// и фабрику клиентов погоды <see cref="IWeatherClientFactory"/>.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации.</param>
    /// <param name="assembly">Сборка в которой располагаются клиенты API погоды.</param>
    public static void AddWeatherApiClients(this IServiceCollection services, Assembly assembly)
    {
        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo<IWeatherApiClient>())
            .As<IWeatherApiClient>()
            .WithTransientLifetime());

        services.AddScoped<IWeatherClientFactory, WeatherClientFactory>();
    }
}
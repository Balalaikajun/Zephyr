using System.Reflection;
using Zephyr.Backend.Utils;
using Zephyr.Backend.Utils.Interfaces;

namespace Zephyr.Backend.Infrastructure.Extensions;

public static class ServiceExtension
{
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
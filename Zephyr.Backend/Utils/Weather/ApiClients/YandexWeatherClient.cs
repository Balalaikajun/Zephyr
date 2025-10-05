using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using Zephyr.Backend.Infrastructure.Extensions;
using Zephyr.Backend.Services.Replies.Core;
using Zephyr.Backend.Utils.Weather.Dtos.YandexWeather;
using Zephyr.Backend.Utils.Weather.Enums;
using Zephyr.Backend.Utils.Weather.Interfaces;

namespace Zephyr.Backend.Utils.Weather.ApiClients;

/// <summary>
///     Клиент для работы с API <see href="https://yandex.ru/dev/weather/">Яндекс.Погода</see>.
/// </summary>
public class YandexWeatherClient(
    HttpClient httpClient,
    IMapper mapper,
    ILogger<OpenMeteoClient> logger)
    : IWeatherApiClient
{
    /// <inheritdoc />
    public WeatherProvider WeatherProvider => WeatherProvider.YandexWeather;

    /// <inheritdoc />
    public async Task<Reply<Models.Weather>> GetCurrentWeather(double latitude, double longitude)
    {
        var url = QueryHelpers.AddQueryString("/v2/forecast", new Dictionary<string, string?>
        {
            ["lat"] = latitude.ToString(CultureInfo.InvariantCulture),
            ["lon"] = longitude.ToString(CultureInfo.InvariantCulture),
            ["limit"] = "1",
            ["hours"] = "false",
            ["extra"] = "false"
        });

        var request = new HttpRequestMessage(HttpMethod.Get, url);

        return await httpClient.GetAndMapAsync<YandexWeatherResponse, Models.Weather>(request, mapper, logger);
    }
}
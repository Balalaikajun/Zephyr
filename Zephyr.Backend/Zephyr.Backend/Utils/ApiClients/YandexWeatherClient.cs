using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Zephyr.Backend.Infrastructure;
using Zephyr.Backend.Infrastructure.Extensions;
using Zephyr.Backend.Models;
using Zephyr.Backend.Services.Replies.Core;
using Zephyr.Backend.Utils.Dtos.YandexWeather;
using Zephyr.Backend.Utils.Enums;
using Zephyr.Backend.Utils.Interfaces;

namespace Zephyr.Backend.Utils.ApiClients;

public class YandexWeatherClient(
    HttpClient httpClient,
    IMapper mapper,
    ILogger<OpenMeteoClient> logger,
    IOptions<Settings> settings,
    IOptions<Secrets> secrets) : IWeatherApiClient
{
    private readonly string _apiKey = secrets.Value.WeatherApiKeys[WeatherProvider];
    private readonly string _baseUrl = settings.Value.WeatherApiBaseUrls[WeatherProvider];
    public static WeatherProvider WeatherProvider => WeatherProvider.YandexWeather;

    public async Task<Reply<Weather>> GetWeather(double latitude, double longitude)
    {
        var url = QueryHelpers.AddQueryString($"{_baseUrl}/v2/forecast", new Dictionary<string, string?>
        {
            ["lat"] = latitude.ToString(CultureInfo.InvariantCulture),
            ["lon"] = longitude.ToString(CultureInfo.InvariantCulture),
            ["limit"] = "1",
            ["hours"] = "false",
            ["extra"] = "false"
        });

        var request = new HttpRequestMessage(HttpMethod.Get, url);

        request.Headers.Add("X-Yandex-Weather-Key", _apiKey);

        return await httpClient.GetAndMapAsync<YandexWeatherResponse, Weather>(request, mapper, logger);
    }
}
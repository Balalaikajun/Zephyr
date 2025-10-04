using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Zephyr.Backend.Infrastructure;
using Zephyr.Backend.Infrastructure.Extensions;
using Zephyr.Backend.Models;
using Zephyr.Backend.Services.Replies.Core;
using Zephyr.Backend.Utils.Dtos.OpenWeather;
using Zephyr.Backend.Utils.Enums;
using Zephyr.Backend.Utils.Interfaces;

namespace Zephyr.Backend.Utils.ApiClients;

public class OpenWeatherClient(
    HttpClient httpClient,
    IMapper mapper,
    ILogger<OpenWeatherClient> logger,
    IOptions<Settings> settings,
    IOptions<Secrets> secrets) : IWeatherApiClient
{
    private readonly string _apiKey = secrets.Value.WeatherApiKeys[WeatherProvider];
    private readonly string _baseUrl = settings.Value.WeatherApiBaseUrls[WeatherProvider];
    public static WeatherProvider WeatherProvider => WeatherProvider.OpenWeather;

    public async Task<Reply<Weather>> GetWeather(double latitude, double longitude)
    {
        var url = QueryHelpers.AddQueryString($"{_baseUrl}/data/2.5/weather", new Dictionary<string, string?>
        {
            ["lat"] = latitude.ToString(CultureInfo.InvariantCulture),
            ["lon"] = longitude.ToString(CultureInfo.InvariantCulture),
            ["appid"] = _apiKey,
            ["units"] = "metric",
            ["lang"] = "ru"
        });

        var request = new HttpRequestMessage(HttpMethod.Get, url);

        return await httpClient.GetAndMapAsync<OpenWeatherResponse, Weather>(request, mapper, logger);
    }
}
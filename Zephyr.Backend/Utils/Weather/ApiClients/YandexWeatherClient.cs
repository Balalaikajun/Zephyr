using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Zephyr.Backend.Infrastructure;
using Zephyr.Backend.Infrastructure.Extensions;
using Zephyr.Backend.Services.Replies.Core;
using Zephyr.Backend.Utils.Weather.Dtos.YandexWeather;
using Zephyr.Backend.Utils.Weather.Enums;
using Zephyr.Backend.Utils.Weather.Interfaces;

namespace Zephyr.Backend.Utils.Weather.ApiClients;

/// <summary>
/// Клиент для работы с API <see href="https://yandex.ru/dev/weather/">Яндекс.Погода</see>.
/// </summary>
public class YandexWeatherClient : IWeatherApiClient
{
    private readonly string _apiKey;
    private readonly string _baseUrl;
    private readonly HttpClient _httpClient;
    private readonly ILogger<OpenMeteoClient> _logger;
    private readonly IMapper _mapper;

    public YandexWeatherClient(HttpClient httpClient,
        IMapper mapper,
        ILogger<OpenMeteoClient> logger,
        Settings settings,
        Secrets secrets)
    {
        _httpClient = httpClient;
        _mapper = mapper;
        _logger = logger;
        _apiKey = secrets.WeatherApiKeys[WeatherProvider];
        _baseUrl = settings.WeatherApiBaseUrls[WeatherProvider];
    }

    /// <inheritdoc/>
    public WeatherProvider WeatherProvider => WeatherProvider.YandexWeather;

    /// <inheritdoc/>
    public async Task<Reply<Models.Weather>> GetCurrentWeather(double latitude, double longitude)
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

        return await _httpClient.GetAndMapAsync<YandexWeatherResponse, Models.Weather>(request, _mapper, _logger);
    }
}
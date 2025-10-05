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

public class YandexWeatherClient : IWeatherApiClient
{
    private readonly string _apiKey;
    private readonly string _baseUrl;
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly ILogger<OpenMeteoClient> _logger;

    public YandexWeatherClient(HttpClient httpClient,
        IMapper mapper,
        ILogger<OpenMeteoClient> logger,
        IOptions<Settings> settings,
        IOptions<Secrets> secrets)
    {
        _httpClient = httpClient;
        _mapper = mapper;
        _logger = logger;
        _apiKey = secrets.Value.WeatherApiKeys[WeatherProvider];
        _baseUrl = settings.Value.WeatherApiBaseUrls[WeatherProvider];
    }

    public WeatherProvider WeatherProvider => WeatherProvider.YandexWeather;

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

        return await _httpClient.GetAndMapAsync<YandexWeatherResponse, Weather>(request, _mapper, _logger);
    }
}
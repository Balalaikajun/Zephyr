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

    public async Task<Reply<Models.Weather>> GetWeather(double latitude, double longitude)
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
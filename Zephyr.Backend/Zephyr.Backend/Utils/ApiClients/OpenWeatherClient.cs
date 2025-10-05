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

public class OpenWeatherClient : IWeatherApiClient
{
    private readonly string _apiKey;
    private readonly string _baseUrl;
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly ILogger<OpenWeatherClient> _logger;

    public OpenWeatherClient(HttpClient httpClient,
        IMapper mapper,
        ILogger<OpenWeatherClient> logger,
        IOptions<Settings> settings,
        IOptions<Secrets> secrets)
    {
        _httpClient = httpClient;
        _mapper = mapper;
        _logger = logger;
        _apiKey = secrets.Value.WeatherApiKeys[WeatherProvider];
        _baseUrl = settings.Value.WeatherApiBaseUrls[WeatherProvider];
    }

    public WeatherProvider WeatherProvider => WeatherProvider.OpenWeather;

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

        return await _httpClient.GetAndMapAsync<OpenWeatherResponse, Weather>(request, _mapper, _logger);
    }
}
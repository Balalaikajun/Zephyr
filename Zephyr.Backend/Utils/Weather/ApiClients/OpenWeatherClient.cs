using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Zephyr.Backend.Infrastructure;
using Zephyr.Backend.Infrastructure.Extensions;
using Zephyr.Backend.Services.Replies.Core;
using Zephyr.Backend.Utils.Weather.Dtos.OpenWeather;
using Zephyr.Backend.Utils.Weather.Enums;
using Zephyr.Backend.Utils.Weather.Interfaces;

namespace Zephyr.Backend.Utils.Weather.ApiClients;

/// <summary>
/// Клиент для работы с API <see href="https://openweathermap.org/">OpenWeather</see>.
/// </summary>
public class OpenWeatherClient : IWeatherApiClient
{
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;
    private readonly ILogger<OpenWeatherClient> _logger;
    private readonly IMapper _mapper;

    public OpenWeatherClient(HttpClient httpClient,
        IMapper mapper,
        ILogger<OpenWeatherClient> logger,
        Secrets secrets)
    {
        _httpClient = httpClient;
        _mapper = mapper;
        _logger = logger;
        _apiKey = secrets.WeatherApiKeys[WeatherProvider];
    }

    /// <inheritdoc/>
    public WeatherProvider WeatherProvider => WeatherProvider.OpenWeather;

    /// <inheritdoc/>
    public async Task<Reply<Models.Weather>> GetCurrentWeather(double latitude, double longitude)
    {
        var url = QueryHelpers.AddQueryString($"/data/2.5/weather", new Dictionary<string, string?>
        {
            ["lat"] = latitude.ToString(CultureInfo.InvariantCulture),
            ["lon"] = longitude.ToString(CultureInfo.InvariantCulture),
            ["appid"] = _apiKey,
            ["units"] = "metric",
            ["lang"] = "ru"
        });

        var request = new HttpRequestMessage(HttpMethod.Get, url);

        return await _httpClient.GetAndMapAsync<OpenWeatherResponse, Models.Weather>(request, _mapper, _logger);
    }
}
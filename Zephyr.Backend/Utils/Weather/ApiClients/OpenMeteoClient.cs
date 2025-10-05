using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Zephyr.Backend.Infrastructure;
using Zephyr.Backend.Infrastructure.Extensions;
using Zephyr.Backend.Services.Replies.Core;
using Zephyr.Backend.Utils.Weather.Dtos.OpenMeteo;
using Zephyr.Backend.Utils.Weather.Enums;
using Zephyr.Backend.Utils.Weather.Interfaces;

namespace Zephyr.Backend.Utils.Weather.ApiClients;

/// <summary>
/// Клиент для работы с API <see href="https://open-meteo.com/">OpenMeteo</see>.
/// </summary>
public class OpenMeteoClient : IWeatherApiClient
{
    private readonly string _baseUrl;
    private readonly HttpClient _httpClient;
    private readonly ILogger<OpenMeteoClient> _logger;
    private readonly IMapper _mapper;

    public OpenMeteoClient(HttpClient httpClient,
        IMapper mapper,
        ILogger<OpenMeteoClient> logger,
        Settings settings)
    {
        _httpClient = httpClient;
        _mapper = mapper;
        _logger = logger;
        _baseUrl = settings.WeatherApiBaseUrls[WeatherProvider];
    }

    /// <inheritdoc/>
    public WeatherProvider WeatherProvider => WeatherProvider.OpenMeteo;

    /// <inheritdoc/>
    public async Task<Reply<Models.Weather>> GetCurrentWeather(double latitude, double longitude)
    {
        var url = QueryHelpers.AddQueryString($"{_baseUrl}/v1/forecast", new Dictionary<string, string?>
        {
            ["latitude"] = latitude.ToString(CultureInfo.InvariantCulture),
            ["longitude"] = longitude.ToString(CultureInfo.InvariantCulture),
            ["current"] = "temperature_2m,relative_humidity_2m,surface_pressure"
        });

        var request = new HttpRequestMessage(HttpMethod.Get, url);

        return await _httpClient.GetAndMapAsync<OpenMeteoResponse, Models.Weather>(request, _mapper, _logger);
    }
}
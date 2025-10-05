using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Zephyr.Backend.Infrastructure;
using Zephyr.Backend.Infrastructure.Extensions;
using Zephyr.Backend.Models;
using Zephyr.Backend.Services.Replies.Core;
using Zephyr.Backend.Utils.Dtos.OpenMeteo;
using Zephyr.Backend.Utils.Enums;
using Zephyr.Backend.Utils.Interfaces;

namespace Zephyr.Backend.Utils.ApiClients;

public class OpenMeteoClient : IWeatherApiClient
{
    private readonly string _baseUrl;
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly ILogger<OpenMeteoClient> _logger;

    public OpenMeteoClient(HttpClient httpClient,
        IMapper mapper,
        ILogger<OpenMeteoClient> logger,
        IOptions<Settings> settings)
    {
        _httpClient = httpClient;
        _mapper = mapper;
        _logger = logger;
        _baseUrl = settings.Value.WeatherApiBaseUrls[WeatherProvider];
    }

    public WeatherProvider WeatherProvider => WeatherProvider.OpenMeteo;

    public async Task<Reply<Weather>> GetWeather(double latitude, double longitude)
    {
        var url = QueryHelpers.AddQueryString($"{_baseUrl}/v1/forecast", new Dictionary<string, string?>
        {
            ["latitude"] = latitude.ToString(CultureInfo.InvariantCulture),
            ["longitude"] = longitude.ToString(CultureInfo.InvariantCulture),
            ["current"] = "temperature_2m,relative_humidity_2m,surface_pressure"
        });

        var request = new HttpRequestMessage(HttpMethod.Get, url);

        return await _httpClient.GetAndMapAsync<OpenMeteoResponse, Weather>(request, _mapper, _logger);
    }
}
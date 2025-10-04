using AutoMapper;
using Microsoft.Extensions.Options;
using Zephyr.Backend.Infrastructure;
using Zephyr.Backend.Models;
using Zephyr.Backend.Utils.Dtos.OpenWeather;
using Zephyr.Backend.Utils.Interfaces;

namespace Zephyr.Backend.Utils;

public class OpenWeatherClient(
    HttpClient httpClient,
    IMapper mapper,
    IOptions<Settings> settings,
    IOptions<Secrets> secrets) : IWeatherApiClient
{
    private readonly string _apiKey = secrets.Value.WeatherApiKeys[WeatherProvider];
    private readonly string _baseUrl = settings.Value.WeatherApiBaseUrls[WeatherProvider];
    public static WeatherProvider WeatherProvider => WeatherProvider.OpenWeather;

    public async Task<Weather> GetWeather(double latitude, double longitude)
    {
        var url = $"{_baseUrl}/data/2.5/weather?lat={latitude}&lon={longitude}&appid={_apiKey}&units=metric&lang=ru";

        var apiData = await httpClient.GetFromJsonAsync<OpenWeatherResponse>(url);

        var weatherData = mapper.Map<Weather>(apiData);

        return weatherData;
    }
}
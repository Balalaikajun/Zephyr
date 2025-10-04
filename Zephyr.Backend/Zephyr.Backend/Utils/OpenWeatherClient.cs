using Microsoft.Extensions.Options;
using Zephyr.Backend.Contracts.Dtos.OpenWeather;
using Zephyr.Backend.Contracts.Interfaces;
using Zephyr.Backend.Infrastructure;
using Zephyr.Backend.Infrastructure.Enums;
using Zephyr.Backend.Models;

namespace Zephyr.Backend.Utils;

public class OpenWeatherClient(
    HttpClient httpClient,
    IOptions<Settings> settings,
    IOptions<Secrets> secrets) : IWeatherApiClient
{
    private readonly string _apiKey = secrets.Value.WeatherApiKeys[WeatherProvider];
    private readonly string _baseUrl = settings.Value.WeatherApiBaseUrls[WeatherProvider];
    public static WeatherProvider WeatherProvider => WeatherProvider.OpenWeather;

    public async Task<Weather> GetWeather(double latitude, double longitude)
    {
        var url = $"{_baseUrl}/data/2.5/weather?lat={latitude}&lon={longitude}&appid={_apiKey}&units=imperial&lang=ru";

        var apiData = await httpClient.GetFromJsonAsync<OpenWeatherResponse>(url);

        var weatherData = new Weather
        {
            Pressure = apiData.Main.Pressure,
            Temperature = apiData.Main.Temp,
            Humidity = apiData.Main.Humidity
        };

        if (weatherData == null)
            throw new NullReferenceException("Failed to deserialize weather data");

        return weatherData;
    }
}
using AutoMapper;
using Zephyr.Backend.Models;
using Zephyr.Backend.Utils.Dtos.OpenMeteo;
using Zephyr.Backend.Utils.Dtos.OpenWeather;
using Zephyr.Backend.Utils.Dtos.YandexWeather;

namespace Zephyr.Backend.Infrastructure.Mapping.WeatherProviders;

public class YandexWeatherMapping : Profile
{
    public YandexWeatherMapping()
    {
        CreateMap<YandexWeatherResponse, Weather>()
            .ForMember(d => d.Humidity, o => o.MapFrom(s => s.Fact.Humidity))
            .ForMember(d => d.Temperature, o => o.MapFrom(s => s.Fact.Temperature))
            .ForMember(d => d.Pressure, o => o.MapFrom(s => s.Fact.Pressure));
    }
}
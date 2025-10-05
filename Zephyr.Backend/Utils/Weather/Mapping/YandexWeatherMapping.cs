using AutoMapper;
using Zephyr.Backend.Utils.Weather.Dtos.YandexWeather;

namespace Zephyr.Backend.Utils.Weather.Mapping;

public class YandexWeatherMapping : Profile
{
    public YandexWeatherMapping()
    {
        CreateMap<YandexWeatherResponse, Models.Weather>()
            .ForMember(d => d.Humidity, o => o.MapFrom(s => s.Fact.Humidity))
            .ForMember(d => d.Temperature, o => o.MapFrom(s => s.Fact.Temperature))
            .ForMember(d => d.Pressure, o => o.MapFrom(s => s.Fact.Pressure));
    }
}
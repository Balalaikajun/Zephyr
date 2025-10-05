using AutoMapper;
using Zephyr.Backend.Models;
using Zephyr.Backend.Utils.Dtos.OpenWeather;

namespace Zephyr.Backend.Infrastructure.Mapping.WeatherProviders;

public class OpenWeatherMapping : Profile
{
    public OpenWeatherMapping()
    {
        CreateMap<OpenWeatherResponse, Weather>()
            .ForMember(d => d.Humidity, o => o.MapFrom(s => s.Main.Humidity))
            .ForMember(d => d.Temperature, o => o.MapFrom(s => s.Main.Temperature))
            .ForMember(d => d.Pressure, o => o.MapFrom(s => s.Main.Pressure));
    }
}
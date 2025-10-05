using AutoMapper;
using Zephyr.Backend.Models;
using Zephyr.Backend.Utils.Dtos.OpenMeteo;
using Zephyr.Backend.Utils.Dtos.OpenWeather;

namespace Zephyr.Backend.Infrastructure.Mapping.WeatherProviders;

public class OpenMeteoMapping : Profile
{
    public OpenMeteoMapping()
    {
        CreateMap<OpenMeteoResponse, Weather>()
            .ForMember(d => d.Humidity, o => o.MapFrom(s => s.Current.Humidity))
            .ForMember(d => d.Temperature, o => o.MapFrom(s => s.Current.Temperature))
            .ForMember(d => d.Pressure, o => o.MapFrom(s => s.Current.Pressure));
    }
}
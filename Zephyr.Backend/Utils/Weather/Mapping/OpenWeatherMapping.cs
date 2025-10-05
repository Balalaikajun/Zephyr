using AutoMapper;
using Zephyr.Backend.Utils.Weather.Dtos.OpenWeather;

namespace Zephyr.Backend.Utils.Weather.Mapping;

public class OpenWeatherMapping : Profile
{
    public OpenWeatherMapping()
    {
        CreateMap<OpenWeatherResponse, Models.Weather>()
            .ForMember(d => d.Humidity, o => o.MapFrom(s => s.Main.Humidity))
            .ForMember(d => d.Temperature, o => o.MapFrom(s => s.Main.Temperature))
            .ForMember(d => d.Pressure, o => o.MapFrom(s => s.Main.Pressure));
    }
}
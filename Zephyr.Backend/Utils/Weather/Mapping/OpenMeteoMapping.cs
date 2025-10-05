using AutoMapper;
using Zephyr.Backend.Utils.Weather.Dtos.OpenMeteo;

namespace Zephyr.Backend.Utils.Weather.Mapping;

public class OpenMeteoMapping : Profile
{
    public OpenMeteoMapping()
    {
        CreateMap<OpenMeteoResponse, Models.Weather>()
            .ForMember(d => d.Humidity, o => o.MapFrom(s => s.Current.Humidity))
            .ForMember(d => d.Temperature, o => o.MapFrom(s => s.Current.Temperature))
            .ForMember(d => d.Pressure, o => o.MapFrom(s => s.Current.Pressure));
    }
}
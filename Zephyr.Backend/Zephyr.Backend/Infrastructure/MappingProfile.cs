using System.Globalization;
using AutoMapper;
using Dadata.Model;
using Zephyr.Backend.Models;

namespace Zephyr.Backend.Infrastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        MapSuggestions();
    }

    private void MapSuggestions()
    {
        CreateMap<Suggestion<Address>, Place>()
            .ForMember(d => d.Name, o => o.MapFrom(s => s.value))
            .ForMember(d => d.Latitude,
                o => o.MapFrom(s => double.Parse(s.data.geo_lat, CultureInfo.InvariantCulture)))
            .ForMember(d => d.Longitude,
                o => o.MapFrom(s => double.Parse(s.data.geo_lon, CultureInfo.InvariantCulture)));
        CreateMap<SuggestResponse<Address>, List<Place>>()
            .ConvertUsing((src, _, ctx) => src.suggestions.Select(s => ctx.Mapper.Map<Place>(s)).ToList());
    }
}
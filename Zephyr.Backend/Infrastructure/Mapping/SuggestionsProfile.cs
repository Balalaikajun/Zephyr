using System.Globalization;
using AutoMapper;
using Dadata.Model;
using Zephyr.Backend.Contracts.Requests.Suggestion;
using Zephyr.Backend.Models;

namespace Zephyr.Backend.Infrastructure.Mapping;

public class SuggestionsProfile : Profile
{
    public SuggestionsProfile()
    {
        MapRequests();
        MapResponses();
        MapImpl();

        void MapRequests()
        {
            CreateMap<GetSuggestRequest, Services.Requests.Suggestion.GetSuggestRequest>();
        }

        void MapResponses()
        {
            CreateMap<Place, Contracts.Responses.Suggestion.Place>();
        }

        void MapImpl()
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
}
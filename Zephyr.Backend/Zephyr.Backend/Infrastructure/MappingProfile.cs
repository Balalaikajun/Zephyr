using System.Globalization;
using AutoMapper;
using Dadata.Model;
using Zephyr.Backend.Contracts.Requests.Shared.Suggestion;
using Zephyr.Backend.Contracts.Requests.Shared.Weather;
using Zephyr.Backend.Contracts.Responses.Core;
using Zephyr.Backend.Models;
using Zephyr.Backend.Services.Replies.Core;
using Zephyr.Backend.Utils.Dtos.OpenWeather;
using Place = Zephyr.Backend.Contracts.Responses.Shared.Suggestion.Place;

namespace Zephyr.Backend.Infrastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        MapCore();
        MapSuggestions();
        MapWeather();
    }

    private void MapCore()
    {
        CreateMap<Error, ErrorResponse>();
    }

    private void MapSuggestions()
    {
        MapRequests();
        MapResponses();
        MapImpl();

        void MapRequests()
        {
            CreateMap<GetSuggestRequest, Services.Requests.GetSuggestRequest>();
        }

        void MapResponses()
        {
            CreateMap<Models.Place, Place>();
        }

        void MapImpl()
        {
            CreateMap<Suggestion<Address>, Models.Place>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.value))
                .ForMember(d => d.Latitude,
                    o => o.MapFrom(s => double.Parse(s.data.geo_lat, CultureInfo.InvariantCulture)))
                .ForMember(d => d.Longitude,
                    o => o.MapFrom(s => double.Parse(s.data.geo_lon, CultureInfo.InvariantCulture)));
            CreateMap<SuggestResponse<Address>, List<Models.Place>>()
                .ConvertUsing((src, _, ctx) => src.suggestions.Select(s => ctx.Mapper.Map<Models.Place>(s)).ToList());
        }
    }

    private void MapWeather()
    {
        MapRequests();
        MapResponses();
        MapImpl();

        void MapRequests()
        {
            CreateMap<GetCurrentWeatherRequest, Services.Requests.GetCurrentWeatherRequest>();
        }

        void MapResponses()
        {
            CreateMap<Weather, Contracts.Responses.Shared.Weather.Weather>();
        }

        void MapImpl()
        {
            CreateMap<OpenWeatherResponse, Weather>()
                .ForMember(d => d.Humidity, o => o.MapFrom(s => s.Main.Humidity))
                .ForMember(d => d.Temperature, o => o.MapFrom(s => s.Main.Temp))
                .ForMember(d => d.Pressure, o => o.MapFrom(s => s.Main.Pressure));
        }
    }
}
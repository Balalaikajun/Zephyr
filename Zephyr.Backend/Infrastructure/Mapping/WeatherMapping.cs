using AutoMapper;
using Zephyr.Backend.Contracts.Requests.Weather;
using Zephyr.Backend.Models;

namespace Zephyr.Backend.Infrastructure.Mapping;

public class WeatherMapping : Profile
{
    public WeatherMapping()
    {
        MapRequests();
        MapResponses();

        void MapRequests()
        {
            CreateMap<GetCurrentWeatherRequest, Services.Requests.Weather.GetCurrentWeatherRequest>();
        }

        void MapResponses()
        {
            CreateMap<Weather, Contracts.Responses.Weather.Weather>();
        }
    }
}
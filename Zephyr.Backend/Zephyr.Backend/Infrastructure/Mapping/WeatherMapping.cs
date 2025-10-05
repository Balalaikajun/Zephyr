using AutoMapper;
using Zephyr.Backend.Contracts.Requests.Shared.Weather;
using Zephyr.Backend.Models;

namespace Zephyr.Backend.Infrastructure.Mapping;

public class WeatherMapping:Profile
{
    public WeatherMapping()
    {
        
        MapRequests();
        MapResponses();
        
        void MapRequests()
        {
            CreateMap<GetCurrentWeatherRequest, Services.Requests.GetCurrentWeatherRequest>();
        }

        void MapResponses()
        {
            CreateMap<Weather, Contracts.Responses.Shared.Weather.Weather>();
        }
    }
}
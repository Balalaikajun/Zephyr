using AutoMapper;
using Zephyr.Backend.Contracts.Responses.Core;
using Zephyr.Backend.Services.Replies.Core;

namespace Zephyr.Backend.Infrastructure.Mapping;

public class CoreMapping : Profile
{
    public CoreMapping()
    {
        CreateMap<Error, ErrorResponse>();
    }
}
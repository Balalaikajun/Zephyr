using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zephyr.Backend.Contracts.Responses.Core;
using Zephyr.Backend.Services.Replies.Core;

namespace Zephyr.Backend.Controllers;

public abstract class BaseController(IMapper mapper) : ControllerBase
{
    protected TServiceRequest MapToServiceRequest<TServiceRequest, THttpRequest>(THttpRequest request)
    {
        return mapper.Map<TServiceRequest>(request);
    }

    protected async Task<IActionResult> Perform<TServiceReply, TServiceRequest, THttpResponse>(TServiceRequest request,
        Func<TServiceRequest, Task<Reply<TServiceReply>>> serviceCall)
    {
        var reply = await serviceCall.Invoke(request);

        if (reply.Error != null) return BadRequest(mapper.Map<ErrorResponse>(reply.Error));

        return Ok(mapper.Map<THttpResponse>(reply.Result));
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zephyr.Backend.Contracts.Responses.Core;
using Zephyr.Backend.Services.Replies.Core;

namespace Zephyr.Backend.Controllers.Core;

/// <summary>
///     Базовый контроллер, предоставляет методы для обработки стандартной логики.
/// </summary>
public abstract class BaseController(IMapper mapper) : ControllerBase
{
    /// <summary>
    ///     Выполняет преобразование HTTP-запроса (Request уровня API)
    ///     в сервисный запрос (Request уровня сервисов).
    /// </summary>
    /// <typeparam name="TServiceRequest">Тип объекта сервисного запроса.</typeparam>
    /// <typeparam name="THttpRequest">Тип объекта HTTP-запроса.</typeparam>
    /// <param name="request">Экземпляр HTTP-запроса.</param>
    /// <returns>Запрос готовый к передаче в сервис.</returns>
    protected TServiceRequest MapToServiceRequest<TServiceRequest, THttpRequest>(THttpRequest request)
    {
        return mapper.Map<TServiceRequest>(request);
    }

    /// <summary>
    ///     Унифицированное выполнение сервисного вызова с маппингом ответа.
    /// </summary>
    /// <typeparam name="TServiceReply">Тип объекта, возвращаемого сервисом.</typeparam>
    /// <typeparam name="TServiceRequest">Тип сервисного запроса.</typeparam>
    /// <typeparam name="THttpResponse">Тип ответа API (DTO для клиента).</typeparam>
    /// <param name="request">Сервисный запрос.</param>
    /// <param name="serviceCall">Функция вызова бизнес-логики.</param>
    /// <returns>Результат выполнения в виде <see cref="IActionResult" />.</returns>
    protected async Task<IActionResult> Perform<TServiceReply, TServiceRequest, THttpResponse>(TServiceRequest request,
        Func<TServiceRequest, Task<Reply<TServiceReply>>> serviceCall)
    {
        var reply = await serviceCall.Invoke(request);

        if (reply.Error != null) return BadRequest(mapper.Map<ErrorResponse>(reply.Error));

        return Ok(mapper.Map<THttpResponse>(reply.Result));
    }
}
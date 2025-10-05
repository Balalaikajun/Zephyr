using AutoMapper;
using Zephyr.Backend.Services.Replies.Core;

namespace Zephyr.Backend.Infrastructure.Extensions;

/// <summary>
///     Расширения для <see cref="HttpClient" />
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>
    ///     Выполняет HTTP GET-запрос и преобразует результат в указанный тип.
    /// </summary>
    /// <typeparam name="TApiResponse">Тип объекта, который возвращает внешний API.</typeparam>
    /// <typeparam name="TDest">Тип объекта, в который нужно преобразовать ответ.</typeparam>
    public static async Task<Reply<TDest>> GetAndMapAsync<TApiResponse, TDest>(
        this HttpClient client,
        HttpRequestMessage request,
        IMapper mapper,
        ILogger logger)
    {
        var response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogError($"Request to {request.RequestUri} failed with status code {response.StatusCode}");

            return Reply<TDest>.Fail(StatusCodes.Status500InternalServerError, "Ошибка запроса к сервису");
        }

        var data = await response.Content.ReadFromJsonAsync<TApiResponse>();

        if (data is null)
            return Reply<TDest>.Fail(StatusCodes.Status500InternalServerError, "Пустой ответ от сервиса");

        var mapped = mapper.Map<TDest>(data);
        return Reply<TDest>.Success(mapped);
    }
}
using AutoMapper;
using Zephyr.Backend.Services.Replies.Core;

namespace Zephyr.Backend.Infrastructure.Extensions;

public static class HttpClientExtensions
{
    public static async Task<Reply<TDest>> GetAndMapAsync<TSource, TDest>(
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

        var data = await response.Content.ReadFromJsonAsync<TSource>();

        if (data is null)
            return Reply<TDest>.Fail(StatusCodes.Status500InternalServerError, "Пустой ответ от сервиса");

        var mapped = mapper.Map<TDest>(data);
        return Reply<TDest>.Success(mapped);
    }
}
using Zephyr.Backend.Contracts.Responses.Core;

namespace Zephyr.Backend.Infrastructure.Middlewares;

/// <summary>
/// Middleware для централизованной обработки исключений в приложении.
/// Перехватывает необработанные исключения, логирует их и формирует стандартный JSON-ответ с кодом ошибки.
/// </summary>
public class HandleExceptionMiddleware(RequestDelegate next, ILogger<HandleExceptionMiddleware> logger)
{
    /// <summary>
    /// Основной метод middleware. Перехватывает исключения при обработке запроса.
    /// </summary>
    /// <param name="context">Контекст текущего HTTP-запроса.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Формирует и отправляет JSON-ответ с информацией об ошибке.
    /// </summary>
    /// <param name="context">Контекст запроса.</param>
    /// <param name="exception">Исключение, которое произошло.</param>
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var message = MapMessage(exception);
        var action = MapAction(exception);
        var statusCode = MapStatusCode(exception);

        var response = new ErrorResponse
        {
            Message = message,
            Action = action,
            Code = statusCode
        };

        context.Response.StatusCode = response.Code;
        context.Response.ContentType = "application/json";

        return context.Response.WriteAsJsonAsync(response);
    }

    /// <summary>
    /// Возвращает текстовое сообщение для клиента в зависимости от типа исключения.
    /// </summary>
    /// <param name="exception">Исключение.</param>
    /// <returns>Краткое описание ошибки.</returns>
    private static string MapMessage(Exception exception)
    {
        return exception switch
        {
            ArgumentNullException => "Отсутствует обязательный параметр.",
            ArgumentException => "Переданы неверные данные.",
            _ => "Неизвестная ошибка сервера."
        };
    }

    /// <summary>
    /// Предлагает действие для пользователя в зависимости от типа исключения.
    /// </summary>
    /// <param name="exception">Исключение.</param>
    /// <returns>Рекомендация по исправлению ошибки или null.</returns>
    private static string? MapAction(Exception exception)
    {
        return exception switch
        {
            ArgumentException => "Проверьте корректность введённых данных.",
            _ => null
        };
    }

    /// <summary>
    /// Определяет HTTP-статус код, соответствующий типу исключения.
    /// </summary>
    /// <param name="exception">Исключение.</param>
    /// <returns>HTTP-статус код для ответа клиенту.</returns>
    private static int MapStatusCode(Exception exception)
    {
        return exception switch
        {
            ArgumentException or ArgumentNullException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}

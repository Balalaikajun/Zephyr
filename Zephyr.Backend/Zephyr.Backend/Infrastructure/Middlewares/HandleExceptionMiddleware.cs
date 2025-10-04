using Zephyr.Backend.Contracts.Responses.Core;

namespace Zephyr.Backend.Infrastructure.Middlewares;

public class HandleExceptionMiddleware(RequestDelegate next, ILogger<HandleExceptionMiddleware> logger)
{
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

    private static string MapMessage(Exception exception) =>
        exception switch
        {
            ArgumentNullException => "Отсутствует обязательный параметр.",
            ArgumentException => "Переданы неверные данные.",
            InvalidOperationException => "Некорректная операция.",
            _ => "Неизвестная ошибка сервиса."
        };

    private static string? MapAction(Exception exception) =>
        exception switch
        {
            ArgumentException => "Проверьте корректность введённых данных.",
            _ => null
        };

    private static int MapStatusCode(Exception exception) =>
        exception switch
        {
            ArgumentException or ArgumentNullException => StatusCodes.Status400BadRequest,
            InvalidOperationException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
}
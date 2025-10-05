namespace Zephyr.Backend.Services.Replies.Core;

/// <summary>
/// Универсальный обёрточный класс для результатов сервисов.
/// Используется для передачи успешного результата (<see cref="Result"/>) 
/// или информации об ошибке (<see cref="Error"/>).
/// </summary>
/// <typeparam name="T">Тип данных успешного результата.</typeparam>
public record Reply<T>
{
    private Reply() { }
    
    /// <summary>
    /// Результат выполнения операции. Заполняется, если операция успешна.
    /// Может быть null, если операция завершилась ошибкой.
    /// </summary>
    public T? Result { get; private init; }

    /// <summary>
    /// Информация об ошибке, если операция завершилась неудачей.
    /// </summary>
    public Error? Error { get; private init; }

    /// <summary>
    /// Создаёт успешный ответ с указанным результатом.
    /// </summary>
    /// <param name="result">Результат операции.</param>
    /// <returns>Экземпляр <see cref="Reply{T}"/> с заполненным <see cref="Result"/>.</returns>
    public static Reply<T> Success(T result)
    {
        return new Reply<T> { Result = result };
    }

    /// <summary>
    /// Создаёт ответ с ошибкой.
    /// </summary>
    /// <param name="code">Код ошибки.</param>
    /// <param name="message">Сообщение об ошибке.</param>
    /// <param name="action">Рекомендованное действие для исправления ошибки.</param>
    /// <returns>Экземпляр <see cref="Reply{T}"/> с заполненным <see cref="Error"/>.</returns>
    public static Reply<T> Fail(int code, string? message = null, string? action = null)
    {
        return new Reply<T>
        {
            Error = new Error
            {
                Code = code,
                Message = message,
                Action = action
            }
        };
    }
}
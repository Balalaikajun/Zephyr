namespace Zephyr.Backend.Services.Replies.Core;

/// <summary>
/// Представляет информацию об ошибке, возвращаемой сервисом.
/// </summary>
public class Error
{
    /// <summary>
    /// Код ошибки.
    /// </summary>
    public int Code { get; init; }

    /// <summary>
    /// Рекомендованное действие для исправления ошибки.
    /// </summary>
    public string? Action { get; init; }

    /// <summary>
    /// Сообщение об ошибке.
    /// </summary>
    public string? Message { get; init; }
}
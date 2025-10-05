namespace Zephyr.Backend.Contracts.Responses.Core;

/// <summary>
///     Ответ Api при возникновении ошибки
/// </summary>
public record ErrorResponse
{
    /// <summary>
    ///     Код ошибки.
    /// </summary>
    public int Code { get; init; }

    /// <summary>
    ///     Рекомендованное действие для исправления ошибки.
    /// </summary>
    public string? Action { get; init; }

    /// <summary>
    ///     Сообщение об ошибке.
    /// </summary>
    public string? Message { get; init; }
}
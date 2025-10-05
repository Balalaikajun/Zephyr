using System.ComponentModel.DataAnnotations;

namespace Zephyr.Backend.Services.Requests.Suggestion;

/// <summary>
/// Запрос для получения подсказок при вводе текста.
/// </summary>
public record GetSuggestRequest
{
    /// <summary>
    /// Строка запроса, по которой нужно сгенерировать подсказки.
    /// </summary>
    public required string Query { get; init; }

    /// <summary>
    /// Количество подсказок, которые нужно вернуть.
    /// </summary>
    public required uint Count { get; init; }
}
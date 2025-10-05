using Zephyr.Backend.Models;
using Zephyr.Backend.Services.Replies.Core;
using Zephyr.Backend.Services.Requests.Suggestion;

namespace Zephyr.Backend.Services.Interfaces;

/// <summary>
///     Сервис для предоставления подсказок при вводе данных
/// </summary>
public interface ISuggestionService
{
    /// <summary>
    ///     Получает список подсказок при вводе места.
    /// </summary>
    /// <param name="request">Запрос на получение подсказки.</param>
    /// <returns>Список подсказок.</returns>
    Task<Reply<List<Place>>> GetPlaceSuggestsAsync(GetSuggestRequest request);
}
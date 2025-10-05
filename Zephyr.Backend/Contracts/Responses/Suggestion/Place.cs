namespace Zephyr.Backend.Contracts.Responses.Suggestion;

/// <summary>
/// Место
/// </summary>
public record Place
{
    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Широта
    /// </summary>
    public required double Latitude { get; init; }

    /// <summary>
    /// Долгота
    /// </summary>
    public required double Longitude { get; init; }
}
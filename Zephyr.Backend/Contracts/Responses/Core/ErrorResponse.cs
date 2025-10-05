namespace Zephyr.Backend.Contracts.Responses.Core;

public record ErrorResponse
{
    public int Code { get; init; }
    public string? Action { get; init; }
    public string? Message { get; init; }
}
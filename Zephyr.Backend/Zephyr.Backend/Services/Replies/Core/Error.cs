namespace Zephyr.Backend.Services.Replies.Core;

public class Error
{
    public int Code { get; init; }
    public string? Action { get; init; }
    public string? Message { get; init; }
}
namespace Zephyr.Backend.Services.Replies.Core;

public record Reply<T>
{
    public T? Result { get; private init; }
    public Error? Error { get; private init; }

    public static Reply<T> Success(T result)
    {
        return new Reply<T> { Result = result };
    }

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
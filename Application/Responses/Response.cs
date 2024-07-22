namespace Application.Responses;

public sealed class Response<TResult1, TResult2>(object? activeResult)
{
    public object? Result { get; init; } = activeResult;

    public static implicit operator Response<TResult1, TResult2>(TResult1? result) => new(result);

    public static implicit operator Response<TResult1, TResult2>(TResult2? result) => new(result);
}
namespace Application.Shared;

public abstract record ErrorResponse(string ErrorMessage)
{
    public sealed record BadRequestErrorResponse(string ErrorMessage) : ErrorResponse(ErrorMessage);
    public sealed record NotFoundErrorResponse(string ErrorMessage) : ErrorResponse(ErrorMessage);
}
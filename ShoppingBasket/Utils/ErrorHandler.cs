using System.Net;
using Application.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingBasket.Utils;

public static class ErrorHandler
{
    public static IActionResult Handle(object? errorResponse)
    {
        var error = errorResponse;

        return error switch
        {
            ErrorResponse.NotFoundErrorResponse response =>
                new NotFoundObjectResult(
                    new ErrorMessage
                    {
                        Message = response.ErrorMessage,
                        Status = (int)HttpStatusCode.NotFound
                    }),
            ErrorResponse.BadRequestErrorResponse response =>
                new BadRequestObjectResult(
                    new ErrorMessage
                    {
                        Message = response.ErrorMessage,
                        Status = (int)HttpStatusCode.BadRequest
                    }),

            _ => throw new InvalidOperationException($"Unexpected error {error}")
        };
    }
}
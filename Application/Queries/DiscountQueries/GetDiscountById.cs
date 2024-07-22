namespace Application.Queries.DiscountQueries;

using Application.Interfaces;
using Application.Responses;
using Application.Shared;
using Domain.Domain;
using MediatR;

public static class GetDiscountById
{
    public record GetDiscountByIdQuery(int Id) : IRequest<Response<Discount?, ErrorResponse>>;

    public class QueryHandler(
        IDiscountRepository discountRepository) : IRequestHandler<GetDiscountByIdQuery, Response<Discount?, ErrorResponse>>
    {
        public async Task<Response<Discount?, ErrorResponse>> Handle(GetDiscountByIdQuery request, CancellationToken cancellationToken)
        {
            var discount = await discountRepository.GetAsync(request.Id);

            if (discount is null)
            {
                return new(
                    new ErrorResponse.NotFoundErrorResponse($"Discount with id {request.Id} was not found."));
            }

            return new(discount);
        }
    }
}
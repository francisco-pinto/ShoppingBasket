namespace Application.Queries.BasketQueries;

using Application.Interfaces;
using Application.Responses;
using Application.Shared;
using Domain.Domain.Basket;
using MediatR;


public static class GetBasketById
{
    public record GetBasketByIdQuery(int Id) : IRequest<Response<Basket?, ErrorResponse>>;
    
    public class QueryHandler(
        IBasketRepository basketRepository) : IRequestHandler<GetBasketByIdQuery, Response<Basket?, ErrorResponse>>
    {
        public async Task<Response<Basket?, ErrorResponse>> Handle(GetBasketByIdQuery request, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasketWithId(request.Id);

            if (basket is null)
            {
                return new Response<Basket?, ErrorResponse>(
                    new ErrorResponse.NotFoundErrorResponse($"Basket with id {request.Id} was not found"));
            }

            return new(basket);
        }
    }
}
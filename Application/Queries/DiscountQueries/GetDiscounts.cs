namespace Application.Queries.DiscountQueries;

using Application.Interfaces;
using Domain.Domain;
using MediatR;

public static class GetDiscounts
{
    public record GetDiscountsQuery() : IRequest<IEnumerable<Discount>>;

    public class QueryHandler(
        IDiscountRepository discountRepository) : IRequestHandler<GetDiscountsQuery, IEnumerable<Discount>>
    {
        public async Task<IEnumerable<Discount>> Handle(GetDiscountsQuery request, CancellationToken cancellationToken)
        {
            return await discountRepository.GetAllAsync();
        }
    }
}
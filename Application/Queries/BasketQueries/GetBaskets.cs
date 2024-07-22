namespace Application.Queries.BasketQueries;

using Application.Interfaces;
using Domain.Domain.Basket;
using MediatR;

public static class GetBaskets
{
    public record GetBasketsQuery() : IRequest<IEnumerable<Basket>>;

    public class QueryHandler(
        IItemRepository itemRepository,
        IBasketRepository basketRepository) : IRequestHandler<GetBasketsQuery, IEnumerable<Basket>>
    {
        public async Task<IEnumerable<Basket>> Handle(GetBasketsQuery request, CancellationToken cancellationToken)
        {
            return await basketRepository.GetAllBaskets();
        }
    }
}
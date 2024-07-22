namespace Application.Commands.BasketCommands;

using Application.Interfaces;
using Application.Utils;
using Domain.Domain.Basket;
using MediatR;

public static class CreateBasket
{
    public record CreateBasketCommand() : IRequest<Basket>;

    public class CommandHandler(
        IClock clock,
        IBasketRepository basketRepository) : IRequestHandler<CreateBasketCommand, Basket>
    {
        public async Task<Basket> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = Basket.Create(clock.Now);

            await basketRepository.AddAsync(basket);

            await basketRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            
            return basket;
        }
    }
}
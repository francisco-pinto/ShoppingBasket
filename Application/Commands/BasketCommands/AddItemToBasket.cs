namespace Application.Commands.BasketCommands;

using Application.Responses;
using Application.Shared;
using Domain.Domain.Basket;
using System.Runtime.Serialization;
using Application.Interfaces;
using MediatR;

public static class AddItemToBasket
{
    public record AddItemToBasketCommand : IRequest<Response<ErrorResponse, Basket>>
    {
        [IgnoreDataMember] public int BasketId { get; set; }
        public int ItemId { get; set; }
    }

    public class CommandHandler(
        IBasketRepository basketRepository,
        IItemRepository itemRepository) : IRequestHandler<AddItemToBasketCommand, Response<ErrorResponse, Basket>>
    {
        public async Task<Response<ErrorResponse, Basket>> Handle(AddItemToBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetAsync(request.BasketId);

            if (basket is null)
            {
                return new Response<ErrorResponse, Basket>(
                    new ErrorResponse.NotFoundErrorResponse($"Basket with id {request.BasketId} not found"));
            }

            if (basket.PurchaseStatus is PurchaseStatus.Purchased)
            {
                return new Response<ErrorResponse, Basket>(
                    new ErrorResponse.BadRequestErrorResponse($"Basket with id {request.BasketId} already purchased"));
            }
            
            var item = await itemRepository.GetAsync(request.ItemId);

            if (item is null)
            {
                return new Response<ErrorResponse, Basket>(
                    new ErrorResponse.NotFoundErrorResponse($"Item with id {request.ItemId} not found"));
            }
            
            basket.AddItem(item);

            await basketRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            
            return basket;
        }
    }
}
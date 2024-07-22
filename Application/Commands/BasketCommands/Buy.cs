namespace Application.Commands.BasketCommands;

using Application.Responses;
using Application.Shared;
using Application.Interfaces;
using MediatR;
using Services.DiscountService;
using Domain.Domain.Basket;

public static class Buy
{
    public record BuyCommand(int BasketId) : IRequest<Response<ErrorResponse, Basket>>;
    public class CommandHandler(
        IBasketRepository basketRepository,
        IDiscountServiceFactory discountServiceFactory) : IRequestHandler<BuyCommand, Response<ErrorResponse, Basket>>
    {
        public async Task<Response<ErrorResponse, Basket>> Handle(BuyCommand request, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasketAsync(request.BasketId);

            if (basket is null)
            {
                return new Response<ErrorResponse, Basket>(
                    new ErrorResponse.NotFoundErrorResponse($"Basket with id {request.BasketId} was not found."));
            }
            
            if (basket.PurchaseStatus is PurchaseStatus.Purchased || basket.Items.Count == 0)
            {
                return new Response<ErrorResponse, Basket>(
                    new ErrorResponse.BadRequestErrorResponse($"Basket with id {request.BasketId} was already purchased"));
            }
            
            foreach (var item in basket.Items)
            {
                if (item.DiscountIsApplied || item.Discount is null)
                {
                    continue;
                }
                
                var discountService = discountServiceFactory.GetDiscountService(item.Discount.DiscountType);

                await discountService!.ApplyDiscountAsync(item);
            }

            basket.Buy();
            
            await basketRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            
            return basket;
        }
    }
}
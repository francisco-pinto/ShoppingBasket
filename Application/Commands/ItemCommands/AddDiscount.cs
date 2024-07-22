namespace Application.Commands.ItemCommands;

using System.Runtime.Serialization;
using Application.Responses;
using Application.Shared;
using Application.Utils;
using Application.Interfaces;
using Domain.Domain;
using MediatR;

public static class AddDiscount
{
    public record AddDiscountCommand : IRequest<Response<ErrorResponse, Item>>
    {
        [IgnoreDataMember] public int ItemId { get; set; }
        public int DiscountId { get; set; }
    }
    
    public class AddDiscountCommandHandler(
        IClock clock,
        IItemRepository itemRepository,
        IDiscountRepository discountRepository) : IRequestHandler<AddDiscountCommand, Response<ErrorResponse, Item>>
    {
        public async Task<Response<ErrorResponse, Item>> Handle(AddDiscountCommand request, CancellationToken cancellationToken)
        {
            var item = await itemRepository.GetAsync(request.ItemId);

            if (item is null)
            {
                return new(
                    new ErrorResponse.NotFoundErrorResponse($"Item with id {request.ItemId} was not found"));
            }

            var discount = await discountRepository.GetAsync(request.DiscountId);

            if (discount is null)
            {
                return new(
                    new ErrorResponse.NotFoundErrorResponse($"Discount with id {request.DiscountId} was not found"));
            }
            
            item.AddDiscount(discount, clock.Now);

            await itemRepository.UpdateAsync(item);

            await itemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new(item);
        }
    }
}
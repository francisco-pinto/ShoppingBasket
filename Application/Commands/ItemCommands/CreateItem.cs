namespace Application.Commands.ItemCommands;

using Application.Responses;
using Application.Shared;
using Application.Interfaces;
using Application.Utils;
using Domain.Domain;
using MediatR;

public static class CreateItem
{
    public record CreateItemCommand(
        int ProductId,
        int Quantity,
        int PriceInCentsByUnity) : IRequest<Response<ErrorResponse, Item>>;

    public class CommandHandler(
        IClock clock,
        IItemRepository itemRepository,
        IProductRepository productRepository) : IRequestHandler<CreateItemCommand, Response<ErrorResponse, Item>>
    {
        public async Task<Response<ErrorResponse, Item>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetAsync(request.ProductId);

            if (product is null)
            {
                return new Response<ErrorResponse, Item>(
                    new ErrorResponse.NotFoundErrorResponse($"Product with id {request.ProductId} does not exists."));
            }
            
            var item = Item.Create(
                product, 
                request.Quantity,
                request.PriceInCentsByUnity,
                clock.Now,
                null);

            await itemRepository.AddAsync(item);

            await itemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new(item);
        }
    }
}
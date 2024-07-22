namespace Application.Commands.DiscountCommands;

using Application.Interfaces;
using Domain.Domain;
using Domain.Domain.DiscountTypes;
using MediatR;

public static class AddQuantityDiscount
{
    public record AddQuantityDiscountCommand(
        DateTime BeginDateTime,
        DateTime EndDateTime,
        Dictionary<int, int> ItemIdDiscount) : IRequest<Discount>;

    public class CommandHandler(
        IDiscountRepository discountRepository,
        IItemRepository itemRepository) : IRequestHandler<AddQuantityDiscountCommand, Discount>
    {
        public async Task<Discount> Handle(AddQuantityDiscountCommand request, CancellationToken cancellationToken)
        {
            var validItemIdDiscount = new List<QuantityDiscount.ItemDiscount>();

            foreach (var itemToBeDiscounted in request.ItemIdDiscount)
            {
                var item = await itemRepository.GetAsync(itemToBeDiscounted.Key);

                if (item is null)
                {
                    continue;
                }
                
                validItemIdDiscount.Add(
                    new QuantityDiscount.ItemDiscount(
                        itemToBeDiscounted.Key, 
                        itemToBeDiscounted.Value));
            }

            if (validItemIdDiscount.Count == 0)
            {
                return null;
            }
            
            var discount = QuantityDiscount.Create(
                request.BeginDateTime,
                request.EndDateTime,
                validItemIdDiscount);

            await discountRepository.AddAsync(discount);

            await discountRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return discount;
        }
    }
}
namespace Application.Services.DiscountService;

using Application.Interfaces;
using Application.Utils;
using Domain.Domain.DiscountTypes;
using Microsoft.Extensions.Logging;
using Domain.Domain;

public class QuantityDiscountService(
    IClock clock,
    ILogger<QuantityDiscountService> logger,
    IDiscountServiceFactory discountServiceFactory,
    IItemRepository itemRepository): IDiscountService
{
    public async Task ApplyDiscountAsync(Item item)
    {
        var currentDate = clock.Now;

        if (item.Discount!.BeginDate > currentDate || item.Discount.EndDate < currentDate)
        {
            return;
        }

        var discount = item.Discount as QuantityDiscount;

        var associatedDiscountItems = await GetAssociatedDiscountItems(discount);

        var discountService = discountServiceFactory.GetDiscountService(DiscountType.PercentageDiscount);
        
        foreach (var associatedDiscountItem in associatedDiscountItems)
        {
            await discountService!.ApplyDiscountAsync(associatedDiscountItem);
        }
        
        item.ApplyDiscount(currentDate);
    }

    private async Task<IEnumerable<Item>> GetAssociatedDiscountItems(QuantityDiscount? discount)
    {
        var associatedDiscountItems = new List<Item>();

        foreach (var associatedDiscountItem in discount!.AssociatedItemDiscounts)
        {
            var discountItem = await itemRepository.GetAsync(associatedDiscountItem.ItemId);

            if (discountItem is null)
            {
                logger.LogWarning($"Item with id {associatedDiscountItem.ItemId} is null");
                continue;
            }
            
            associatedDiscountItems.Add(discountItem);
        }
        
        return associatedDiscountItems;
    }
}
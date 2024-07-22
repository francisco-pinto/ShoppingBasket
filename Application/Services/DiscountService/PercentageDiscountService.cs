namespace Application.Services.DiscountService;
using Domain.Domain;
using Utils;

public class PercentageDiscountService(IClock clock): IDiscountService
{
    public Task ApplyDiscountAsync(Item item)
    {
        var currentDate = clock.Now;

        if (item.Discount!.BeginDate > currentDate || item.Discount.EndDate < currentDate)
        {
            return Task.FromResult(item.TotalPriceInCents);
        }

        item.ApplyDiscount(currentDate);

        return Task.CompletedTask;
    }
}
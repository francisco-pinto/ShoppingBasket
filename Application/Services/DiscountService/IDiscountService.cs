namespace Application.Services.DiscountService;

using Domain.Domain;

public interface IDiscountService
{
    public Task ApplyDiscountAsync(Item item);
}
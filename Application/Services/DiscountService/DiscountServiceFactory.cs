namespace Application.Services.DiscountService;

using Domain.Domain.DiscountTypes;
using Microsoft.Extensions.DependencyInjection;
public interface IDiscountServiceFactory
{
    IDiscountService? GetDiscountService(DiscountType discountType);
}

public class DiscountServiceFactory : IDiscountServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DiscountServiceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IDiscountService? GetDiscountService(DiscountType discountType)
    {
        return discountType switch
        {
            DiscountType.QuantityDiscount => _serviceProvider.GetService<QuantityDiscountService>(),
            DiscountType.PercentageDiscount => _serviceProvider.GetService<PercentageDiscountService>(),
            _ => throw new ArgumentException("Invalid discount type")
        };
    }
}
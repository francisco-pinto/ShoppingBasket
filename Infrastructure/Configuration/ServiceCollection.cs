using Infrastructure.Repositories.DiscountRepository;

namespace Infrastructure.Configuration;

using Application.Interfaces;
using Application.Utils;
using Infrastructure.Repositories.BasketRepository;
using Infrastructure.Repositories.ProductRepository;
using Infrastructure.Repositories.ItemRepository;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollection
{
    public static void RegisterInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IClock, Clock>();
        
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IDiscountRepository, DiscountRepository>();
    }
}
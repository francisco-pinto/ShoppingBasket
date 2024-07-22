namespace Application.Configuration;

using System.Reflection;
using Application.Services.DiscountService;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollection
{
    public static void RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        services.AddScoped<IDiscountService, QuantityDiscountService>();
        services.AddScoped<IDiscountService, PercentageDiscountService>();
        services.AddScoped<IDiscountServiceFactory, DiscountServiceFactory>();
    }
}
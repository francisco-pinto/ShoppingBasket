using Application.Commands.ProductCommands;
using Application.Configuration;
using Infrastructure;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ShoppingBasket;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Exceptions;
using System.Reflection;
using Microsoft.OpenApi.Models;
using SwashbuckleConfigurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System;
using System.IO;

public class Startup(IConfiguration configuration)
{
    private IConfiguration Configuration = configuration;
    public void Configure(IApplicationBuilder app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shift Management API V1"));

        app.UseExceptionMiddleware();

        app.UseRouting();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging();
        
        services.RegisterApplicationServices();
        services.RegisterInfrastructureServices();
        
        services.AddDbContext<ShoppingBasketDbContext>(options =>
            options.UseMySql(this.Configuration.GetConnectionString("DefaultConnection"), 
                new MySqlServerVersion(new Version(8, 0, 21))));
        
        services.AddControllers()
            .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            });
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
        services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "Shopping Basket API", 
                    Version = "v1", 
                    Description = "API that manages a shopping basket application."});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                c.DescribeAllParametersInCamelCase();
                
                c.SchemaFilter<HidePropertyFilter>();
                c.SchemaFilter<RequireNonNullablePropertiesSchemaFilter>();   
                c.SupportNonNullableReferenceTypes();         
                c.UseAllOfToExtendReferenceSchemas();
                c.UseAllOfForInheritance(); 
            });
        
    }
}
using Application.Products.Services.Abstract;
using Application.Products.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureApplicationServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
    }
}
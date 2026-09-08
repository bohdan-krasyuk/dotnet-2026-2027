using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureInfrastructureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddPersistenceServices();
    }
}
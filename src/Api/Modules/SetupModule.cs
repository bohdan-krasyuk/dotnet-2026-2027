namespace Api.Modules;

public static class SetupModule
{
    public static void SetupServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddCors();
    }

    private static void AddCors(this IServiceCollection services)
    {
        services.AddCors(options =>
            options.AddDefaultPolicy(policy =>
                policy.SetIsOriginAllowed(_ => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()));
    }
}
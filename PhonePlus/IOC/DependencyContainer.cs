namespace PhonePlus.IOC;

public static class DependencyContainer
{
    public static IServiceCollection AddDependencyContainer(this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
    {
        services.AddApplicationDependencies();
        services.AddInterfaceDependencies(configuration);
        services.AddInfrastructure(configuration, builder);
        return services;
    }
}
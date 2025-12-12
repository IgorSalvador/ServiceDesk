using Microsoft.Extensions.DependencyInjection;

namespace ServiceDesk.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssemblies(typeof(ApplicationConfiguration).Assembly));

        return services;
    }
}

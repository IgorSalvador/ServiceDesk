using Microsoft.Extensions.DependencyInjection;

namespace ServiceDesk.Application;

public static class ApplicationConfiguration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplication()
        {
            services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(ApplicationConfiguration).Assembly));

            return services;
        }
    }
}

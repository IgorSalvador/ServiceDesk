using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.Application.Common.Interfaces;
using ServiceDesk.Domain.Entities;
using ServiceDesk.Infrastructure.Data;
using ServiceDesk.Infrastructure.Data.Identity;
using ServiceDesk.Infrastructure.Data.Seeds;

namespace ServiceDesk.Infrastructure;

public static class InfrastructureConfiguration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure(IConfiguration configuration)
        {
            services
                .AddContext(configuration)
                .AddIdentity()
                .AddServices();

            return services;
        }

        public IServiceCollection AddContext(IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
            });

            return services;
        }

        public IServiceCollection AddIdentity()
        {
            services.AddScoped<DbInitializer>();

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders(); ;

            return services;
        }

        public IServiceCollection AddServices()
        {
            services.AddScoped<ITokenService, JwtTokenService>();

            return services;
        }

        public IServiceCollection AddRepositories()
        {
            // Adicionar repositórios quando implementar

            return services;
        }
    }
}

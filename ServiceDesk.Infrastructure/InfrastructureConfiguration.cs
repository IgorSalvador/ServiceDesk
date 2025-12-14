using Microsoft.AspNetCore.Builder;
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
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder AddInfrastructure()
        {
            builder.AddContext();
            builder.AddIdentity();
            builder.AddServices();
            builder.AddRepositories();

            return builder;
        }

        public void AddContext()
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
            });
        }

        public void AddIdentity()
        {
            builder.Services.AddScoped<DbInitializer>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
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
        }

        public void AddServices()
        {
            builder.Services.AddScoped<ITokenService, JwtTokenService>();
        }

        public void AddRepositories()
        {
            // Adicionar repositórios quando implementar
        }
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

namespace ServiceDesk.Api
{
    public static class ApiConfiguration
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddApiConfiguration(IConfiguration configuration)
            {
                services
                    .AddSwaggerConfiguration()
                    .AddJwtAuthentication(configuration);


                return services;
            }

            private IServiceCollection AddSwaggerConfiguration()
            {
                services.AddEndpointsApiExplorer();

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "HDE Helpdesk Enterprise API",
                        Version = "v1",
                        Description = "API de Gestão de Chamados e Workflows Corporativos",
                        Contact = new OpenApiContact
                        {
                            Name = "Equipe de Arquitetura",
                            Email = "arquitetura@sdesk.com"
                        }
                    });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Informe: Bearer {seu_token}"
                    });

                    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecuritySchemeReference("bearer", document)] = []
                    });
                });

                return services;
            }

            public IServiceCollection AddJwtAuthentication(IConfiguration configuration)
            {
                var secretKey = configuration["Jwt:Secret"];

                if (string.IsNullOrEmpty(secretKey))
                    throw new ArgumentNullException("Jwt:Secret", "A chave JWT não foi configurada no appsettings.json");

                var key = Encoding.ASCII.GetBytes(secretKey);

                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;

                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["Jwt:Audience"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

                return services;
            }
        }

        extension(WebApplication app)
        {
            public void ConfigureDevEnvironment()
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapSwagger().RequireAuthorization();
            }

            public void UseSecurity()
            {
                app.UseAuthentication();
                app.UseAuthorization();
            }
        }
    }
}

using Microsoft.OpenApi;

namespace ServiceDesk.Api
{
    public static class ApiConfiguration
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddApiConfiguration()
            {
                services.AddSwaggerConfiguration();

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
        }
    }
}

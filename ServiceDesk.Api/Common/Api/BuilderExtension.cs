using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

namespace ServiceDesk.Api.Common.Api;

public static class BuilderExtension
{
    extension(WebApplicationBuilder builder)
    {
        public void AddConfiguration()
        {
            builder.Services.AddControllers();

            ApiConfiguration.BackendUrls = builder.Configuration.GetSection("BackendUrls").Get<List<string>>() ?? [];
            ApiConfiguration.FrontendUrls = builder.Configuration.GetSection("FrontendUrls").Get<List<string>>() ?? [];
        }

        public void AddDocumentation()
        {
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Helpdesk Enterprise API",
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
        }

        public void AddJwtAuthentication()
        {
            var secretKey = builder.Configuration["Jwt:Secret"] ?? string.Empty;

            if (string.IsNullOrEmpty(secretKey))
                throw new ArgumentNullException("Jwt:Secret", "A chave JWT não foi configurada no appsettings.json");

            var key = Encoding.ASCII.GetBytes(secretKey);

            builder.Services.AddAuthentication(x =>
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
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public void AddCrossOrigin()
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(ApiConfiguration.CorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            ApiConfiguration.BackendUrls[0],
                            ApiConfiguration.BackendUrls[1],
                            ApiConfiguration.FrontendUrls[0],
                            ApiConfiguration.FrontendUrls[1]
                        )
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });
        }
    }
}

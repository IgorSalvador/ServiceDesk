using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.Application.Common.Behaviors;
using System.Reflection;

namespace ServiceDesk.Application;

public static class ApplicationConfiguration
{
    extension(WebApplicationBuilder builder)
    {
        public void AddApplication()
        {
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(ApplicationConfiguration).Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

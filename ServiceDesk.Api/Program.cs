using Microsoft.AspNetCore.Identity;
using ServiceDesk.Api;
using ServiceDesk.Api.Extensions;
using ServiceDesk.Application;
using ServiceDesk.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.ConfigureDevEnvironment();
app.UseHttpsRedirection();
app.UseSecurity();
app.MapControllers();
await app.UseDatabaseSeeding();
app.Run();

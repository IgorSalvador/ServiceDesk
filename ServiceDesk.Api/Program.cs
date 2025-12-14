using Microsoft.AspNetCore.Identity;
using ServiceDesk.Api.Common.Api;
using ServiceDesk.Api.Common.Data;
using ServiceDesk.Application;
using ServiceDesk.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.AddConfiguration();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddJwtAuthentication();

builder.AddInfrastructure();
builder.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCrossOrigin();
app.UseHttpsRedirection();
app.UseSecurity();
app.MapControllers();
await app.UseDatabaseSeeding();
app.Run();

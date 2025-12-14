namespace ServiceDesk.Api.Common.Api;

public static class AppExtension
{
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

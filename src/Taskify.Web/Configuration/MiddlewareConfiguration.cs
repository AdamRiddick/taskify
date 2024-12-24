namespace Taskify.Web.Configuration;

using FastEndpoints;
using FastEndpoints.Swagger;

public static class MiddlewareConfiguration
{
    public static IApplicationBuilder UseMiddlewares(
        this WebApplication app,
        ILogger logger)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseDefaultExceptionHandler(); // from FastEndpoints
            app.UseHsts();
        }

        app.UseFastEndpoints()
            .UseSwaggerGen(); // Includes AddFileServer and static files middleware

        app.UseHttpsRedirection(); // Note this will drop Authorization headers
        logger.LogInformation("Middlewares registered.");
        return app;
    }
}

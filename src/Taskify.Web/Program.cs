using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;

using Taskify.Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
var logger = builder.AddLoggerConfiguration();
var taskifyEnvironmentSettings = builder.Services.AddSettings(builder.Environment, logger);

builder.Services
        .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApp(builder.Configuration, "AzureAd");

builder.Services
       .AddMediatrConfiguration()
       .AddServices(builder.Configuration, taskifyEnvironmentSettings, logger)
       .AddTaskifyAuthorization();

// Configure the HTTP request pipeline
var app = builder.Build();
app.UseMiddlewares(logger);

// Let's do this!
await app.RunAsync();

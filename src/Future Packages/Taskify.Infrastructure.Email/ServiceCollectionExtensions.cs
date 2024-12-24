namespace Taskify.Infrastructure.Email;

using Microsoft.Extensions.DependencyInjection;

using Taskify.SharedKernel.Configuration;
using Taskify.SharedKernel.Email;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEmail(
        this IServiceCollection services,
        ITaskifyEnvironmentSettings taskifyEnvironmentSettings)
    {
        if (taskifyEnvironmentSettings.IsDevelopment)
        {
            services.AddTransient<IEmailSender, FakeEmailSender>();
        } 
        else
        {
            services.AddTransient<IEmailSender, SmtpEmailSender>();
        }

        return services;
    }
}

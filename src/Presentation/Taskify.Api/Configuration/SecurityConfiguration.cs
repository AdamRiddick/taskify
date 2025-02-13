namespace Taskify.Api.Configuration;

using Microsoft.AspNetCore.Authorization;

using Taskify.Api.Authorization;
using Taskify.Api.Authorization.Handlers;
using Taskify.Api.Authorization.Requirements;

public static class SecurityConfiguration
{
    public static IServiceCollection AddTaskifyAuthorization(
        this IServiceCollection services)
    {
        services.AddTransient<IAuthorizationHandler, HasRoleAccessToContextHandler>();
        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                PolicyNames.HasRoleAccessToContext,
                policy => policy
                            .RequireAuthenticatedUser()
                            .AddRequirements(new HasRoleAccessToContextRequirement()));

            options.AddPolicy(
                PolicyNames.HasScope,
                policy => policy
                            .RequireAuthenticatedUser()
                            .AddRequirements(new HasScopeRequirement()));

            options.FallbackPolicy = options.GetPolicy(PolicyNames.HasRoleAccessToContext);
        });

        return services;
    }
}
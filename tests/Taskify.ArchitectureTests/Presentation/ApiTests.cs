namespace Taskify.ArchitectureTests.Presentation;

using System.Reflection;

using Taskify.Api.Authorization;
using Taskify.ArchitectureTests.Common;

public class ApiTests : ClientTestsBase<ApiTests>
{
    static ApiTests()
    {
        var assembliesToCheck = new Assembly[]
        {
            typeof(PolicyNames).Assembly, // Taskify.Api
        };

        var allowedReferences = new List<string>
        {
            "FastEndpoints",

            "Microsoft.AspNetCore",
            "Microsoft.Extensions",

            "Serilog",

            "Taskify.Infrastructure",
            "Taskify.Identity",
            "Taskify.Tasks"
        };

        Init("Api", assembliesToCheck, allowedReferences);
    }
}

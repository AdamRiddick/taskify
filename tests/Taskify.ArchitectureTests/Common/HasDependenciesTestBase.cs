namespace Taskify.ArchitectureTests.Common;

using System.Collections.Generic;

using ArchUnitNET.Domain.Extensions;

public abstract class HasDependenciesTestBase
{
    /// <summary>
    /// A list of dependencies that are acceptable in any domain or layer.
    /// </summary>
    protected readonly List<string> GloballyAllowedDependencies =
        [
            // System Packages
            "System",

            // Microsoft Packages
            "Microsoft.CodeAnalysis",
            "Microsoft.Extensions.Configuration",
            "Microsoft.Extensions.DependencyInjection",

            // Internal Packages
            "Taskify.SharedKernel",

            // External Packages
            "Ardalis.GuardClauses",
            "Ardalis.GuardClauses.Guard",
            "Ardalis.GuardClauses.GuardClauseExtensions",
            "Ardalis.Result",
            "FluentValidation",
            "Mapster",
            "MediatR",
        ];

    public List<string> BuildDependencies(
            params List<string>[] dependencies)
    {
        List<string> result = [.. GloballyAllowedDependencies];
        dependencies.ForEach(result.AddRange);
        return result;
    }
}

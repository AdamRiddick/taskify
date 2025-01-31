namespace Taskify.ArchitectureTests.Common;

using ArchUnitNET.Loader;
using ArchUnitNET.Domain;
using System.Collections.Generic;
using System;

using Assembly = System.Reflection.Assembly;
using ArchUnitNET.Domain.Extensions;

#pragma warning disable S2326 // Unused type parameters should be removed
public abstract class TestBase<T> where T : class
#pragma warning restore S2326 // Unused type parameters should be removed

{
#pragma warning disable S2743 // Static fields should not be used in generic types
    protected static Architecture? Architecture = null;
#pragma warning restore S2743 // Static fields should not be used in generic types

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

    protected TestBase()
    {
        if (Architecture == null)
        {
            throw new Exception("Architecture has not been initialised. Call Init()");
        }
    }

    protected static void Init(Assembly[] assemblies)
    {
        Architecture = new ArchLoader().LoadAssemblies(assemblies).Build();
    }

    public List<string> BuildDependencies(
            params List<string>[] dependencies)
    {
        List<string> result = new(GloballyAllowedDependencies);
        dependencies.ForEach(result.AddRange);
        return result;
    }
}

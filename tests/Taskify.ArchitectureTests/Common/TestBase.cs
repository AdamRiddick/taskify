namespace Taskify.ArchitectureTests.Common;

using ArchUnitNET.Loader;
using ArchUnitNET.Domain;
using System.Collections.Generic;
using System;

using Assembly = System.Reflection.Assembly;
using ArchUnitNET.Domain.Extensions;

public abstract class TestBase
{
    protected static Architecture? Architecture = null;

    /// <summary>
    /// A list of dependencies that are acceptable in any domain or layer.
    /// </summary>
    protected readonly List<string> GloballyAllowedDependencies =
        [
            // System Packages
            "System",

            // Microsoft Packages
            "Microsoft.CodeAnalysis",

            // Internal Packages
            "Taskify.SharedKernel",

            // External Packages
            "Ardalis.GuardClauses",
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

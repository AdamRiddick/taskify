namespace Taskify.ArchitectureTests.Common;

using ArchUnitNET.Loader;
using ArchUnitNET.Domain;
using System;

using Assembly = System.Reflection.Assembly;

#pragma warning disable S2326 // Unused type parameters should be removed
public abstract class TestBase<T> : HasDependenciesTestBase where T : class
#pragma warning restore S2326 // Unused type parameters should be removed

{
#pragma warning disable S2743 // Static fields should not be used in generic types
    protected static Architecture? Architecture = null;
#pragma warning restore S2743 // Static fields should not be used in generic types

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
}

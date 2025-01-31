namespace Taskify.ArchitectureTests.Common;

using ArchUnitNET.xUnit;

using System;
using System.Collections.Generic;

using Xunit;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

using Assembly = System.Reflection.Assembly;

/// <remarks>
/// This class is generic to take advantage of the fact that static members are not shared between
/// derived types. For performance, we want to share the static members across instances of a derived type 
/// but kept unique per derived type.
/// See here: https://stackoverflow.com/a/49582829/295813
/// Because of this, we've had to disable some warnings below.
/// </remarks>
#pragma warning disable S2326 // Unused type parameters should be removed
public abstract class DomainTestsBase<T> : TestBase<T> where T : class
#pragma warning disable S2326 // Unused type parameters should be removed
{
#pragma warning disable S2743 // Static fields should not be used in generic types
    private static bool _initialised = false;

    protected static string _domain = string.Empty;
#pragma warning restore S2743 // Static fields should not be used in generic types

    /// <summary>
    /// Constructor
    /// </summary>
    /// <exception cref="Exception">Ensure Init() has been called in derived class static constructor.</exception>
    protected DomainTestsBase()
        : base()
    {
        if (!_initialised)
        {
            throw new Exception("Domain has not been initialised. Call DomainTestsBase.Init(domain, assemblies)");
        }
    }

    protected static void Init(string domain, Assembly[] assemblies)
    {
        _domain = domain;
        Init(assemblies);
        _initialised = true;
    }

    [Fact]
    public void core_dependencies_check()
    {
        var namespaceToTest = $"Taskify.{_domain}.Core";
        var approvedReferences = new List<string>() {
            namespaceToTest
        };

        var rejectedReferences = new List<string>() {
            "Microsoft.EntityFrameworkCore",
            $"Taskify.{_domain}.Infrastructure",
            "Taskify.Infrastructure.Ef",
            "Taskify.Infrastructure.Email",
            "\bTest\b"
        };

        var rule = Types()
                   .That()
                   .ResideInNamespace(namespaceToTest, true)
                   .Should()
                   .OnlyDependOn(BuildDependencies(approvedReferences), true)
                   .AndShould()
                   .NotDependOnAny(rejectedReferences, true);

        rule.Check(Architecture);
    }

    [Fact]
    public void infrastructure_dependencies_check()
    {
        var namespaceToTest = $"Taskify.{_domain}.Infrastructure";
        var approvedReferences = new List<string>() 
        {
            namespaceToTest,
            $"Taskify.{_domain}.Core",
            "Microsoft.EntityFrameworkCore",
            "Taskify.Infrastructure.Ef",
            "Taskify.Infrastructure.Email"
        };

        var rejectedReferences = new List<string>() {
            "\bTest\b"
        };

        var rule = Types()
                   .That()
                   .ResideInNamespace(namespaceToTest, true)
                   .Should()
                   .OnlyDependOn(BuildDependencies(approvedReferences), true)
                   .AndShould()
                   .NotDependOnAny(rejectedReferences, true);

        rule.Check(Architecture);
    }

    [Fact]
    public void use_cases_dependencies_check()
    {
        var namespaceToTest = $"Taskify.{_domain}.UseCases";
        var approvedReferences = new List<string>() {
            namespaceToTest,
            $"Taskify.{_domain}.Core"
        };
        
        var rejectedReferences = new List<string>() {
            $"Taskify.{_domain}.Infrastructure",
            "Taskify.Infrastructure.Ef",
            "\bTest\b"
        };

        var rule = Types()
                   .That()
                   .ResideInNamespace(namespaceToTest, true)
                   .Should()
                   .OnlyDependOn(BuildDependencies(approvedReferences), true)
                   .AndShould()
                   .NotDependOnAny(rejectedReferences, true);

        rule.Check(Architecture);
    }

    [Fact]
    public void top_level_namespace_check()
    {
        var rule = Types()
                    .That()
                    .HaveFullNameContaining($"Taskify.{_domain}")
                    .Should()
                    .ResideInNamespace($"Taskify.{_domain}.Core", true)
                    .OrShould()
                    .ResideInNamespace($"Taskify.{_domain}.Infrastructure", true)
                    .OrShould()
                    .ResideInNamespace($"Taskify.{_domain}.UseCases", true);

        rule.Check(Architecture);
    }
}

namespace Taskify.ArchitectureTests.Common;

using ArchUnitNET.xUnit;

using System;
using System.Collections.Generic;

using Xunit;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

using Assembly = System.Reflection.Assembly;

public abstract class DomainTestsBase : TestBase
{
    private static bool _initialised = false;

    protected static string _domain = string.Empty;

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

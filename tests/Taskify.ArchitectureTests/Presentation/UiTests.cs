namespace Taskify.ArchitectureTests.Common;

using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;

using System.Collections.Generic;

using Taskify.Ui;

using Xunit;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

using Assembly = System.Reflection.Assembly;
public class UiTests : HasDependenciesTestBase
{
    private readonly Architecture _architecture;

    public UiTests()
    {
        var assemblies = new Assembly[]
        {
            typeof(App).Assembly, // Taskify.Ui
        };

        _architecture = new ArchLoader().LoadAssemblies(assemblies).Build();
    }

    [Fact]
    public void dependencies_check()
    {
        var namespaceToTest = $"Taskify.Ui";
        var allowedReferences = new List<string>() {
            namespaceToTest,
            "Microsoft.AspNetCore.Components"
        };

        var rejectedReferences = new List<string>() {
            "\bTest\b"
        };

        var rule = Types()
                   .That()
                   .ResideInNamespace(namespaceToTest, true)
                   .Should()
                   .OnlyDependOn(BuildDependencies(allowedReferences), true)
                   .AndShould()
                   .NotDependOnAny(rejectedReferences, true);

        rule.Check(_architecture);
    }
}

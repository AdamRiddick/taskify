namespace Taskify.ArchitectureTests.Future_Packages;

using ArchUnitNET.xUnit;

using System.Collections.Generic;
using System.Reflection;

using Taskify.ArchitectureTests.Common;
using Taskify.Infrastructure.Ef;

using Xunit;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

public class TaskifyInfrastructureEfTests : TestBase<TaskifyInfrastructureEfTests>
{
    static TaskifyInfrastructureEfTests()
    {
        var assembliesToCheck = new Assembly[]
        {
            typeof(EntityBase).Assembly // Taskify.Infrastructure.Ef
        };

        Init(assembliesToCheck);
    }
    [Fact]
    public void dependencies_check()
    {
        var namespaceToTest = "Taskify.Infrastructure.Ef";
        var approvedReferences = new List<string>() {
            namespaceToTest,
            "Taskify.SharedKernel",
            "Microsoft.Extensions",
            "Microsoft.EntityFrameworkCore",
            "System"
        };

        var rejectedReferences = new List<string>() {
            "^Taskify\\.\\w+\\.Core$",
            "^Taskify\\.\\w+\\.Infrastructure",
            "^Taskify\\.\\w+\\.UseCases",
            "Taskify.Web",
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
}

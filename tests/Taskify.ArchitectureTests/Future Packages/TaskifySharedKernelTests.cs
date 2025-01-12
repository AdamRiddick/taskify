namespace Taskify.ArchitectureTests.Future_Packages;

using ArchUnitNET.xUnit;

using System.Collections.Generic;
using System.Reflection;

using Taskify.ArchitectureTests.Common;
using Taskify.SharedKernel.Configuration;

using Xunit;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

public class TaskifySharedKernelTests : TestBase
{
    static TaskifySharedKernelTests()
    {
        var assembliesToCheck = new Assembly[]
        {
            typeof(ITaskifyEnvironmentSettings).Assembly // Taskify.SharedKernel
        };

        Init(assembliesToCheck);
    }
    [Fact]
    public void dependencies_check()
    {
        var namespaceToTest = "Taskify.SharedKernel";
        var approvedReferences = new List<string>() {
            namespaceToTest,
            "Ardalis.GuardClauses",
            "FluentValidation",
            "MediatR",
            "Microsoft.Extensions",
            "System"
        };

        var rejectedReferences = new List<string>() {
            "^Taskify\\.(?!.*SharedKernel$).*\r\n",
            "\bTest\b"
        };

        var rule = Types()
                   .That()
                   .ResideInNamespace(namespaceToTest, true)
                   .Should()
                   .OnlyDependOn(approvedReferences, true)
                   .AndShould()
                   .NotDependOnAny(rejectedReferences, true);

        rule.Check(Architecture);
    }
}

namespace Taskify.ArchitectureTests.Future_Packages;

using ArchUnitNET.xUnit;

using System.Collections.Generic;
using System.Reflection;

using Taskify.ArchitectureTests.Common;
using Taskify.Infrastructure.Email;

using Xunit;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

public class TaskifyInfrastructureEmailTests : TestBase
{
    static TaskifyInfrastructureEmailTests()
    {
        var assembliesToCheck = new Assembly[]
        {
            typeof(FakeEmailSender).Assembly // Taskify.Infrastructure.Email
        };

        Init(assembliesToCheck);
    }
    [Fact]
    public void dependencies_check()
    {
        var namespaceToTest = "Taskify.Infrastructure.Email";
        var approvedReferences = new List<string>() {
            namespaceToTest,
            "Taskify.SharedKernel",
            "Microsoft.Extensions",
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
                   .OnlyDependOn(approvedReferences, true)
                   .AndShould()
                   .NotDependOnAny(rejectedReferences, true);

        rule.Check(Architecture);
    }
}

namespace Taskify.ArchitectureTests.Domains
{
    using System.Reflection;

    using Taskify.ArchitectureTests.Common;

    public class IdentityTests : DomainTestsBase
    {
        static IdentityTests()
        {
            var assembliesToCheck = new Assembly[]
            {
            };

            Init("Identity", assembliesToCheck);
        }
    }
}

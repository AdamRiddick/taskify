namespace Taskify.ArchitectureTests.Domains
{
    using System.Reflection;

    using Taskify.ArchitectureTests.Common;
    using Taskify.Identity.Core.ContextTypeAggregate;
    using Taskify.Identity.Infrastructure.Data;
    using Taskify.Identity.UseCases.ContextTypes.Create;

    public class IdentityTests : DomainTestsBase<IdentityTests>
    {
        static IdentityTests()
        {
            var assembliesToCheck = new Assembly[]
            {
                typeof(ContextType).Assembly, // Taskify.Identity.Core
                typeof(IdentityDbContext).Assembly, // Taskify.Identity.Infrastructure
                typeof(CreateContextTypeCommand).Assembly  // Taskify.Identity.UseCases
            };

            Init("Identity", assembliesToCheck);
        }
    }
}

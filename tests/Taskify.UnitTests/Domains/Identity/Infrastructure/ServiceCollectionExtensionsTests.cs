namespace Taskify.UnitTests.Domains.Identity.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Moq;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.Identity.Infrastructure;
using Taskify.Identity.Infrastructure.Data;
using Taskify.SharedKernel;
using Taskify.SharedKernel.Configuration;
using Taskify.SharedKernel.Data;
using Taskify.UnitTests.Helpers;

using Xunit;

public class ServiceCollectionExtensionsTests
{
    public class AddIdentityInfrastructureTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration = new Mock<IConfiguration>();

        private readonly Mock<ITaskifyEnvironmentSettings> _mockTaskifyEnvironmentSettings = new Mock<ITaskifyEnvironmentSettings>();

        [Fact]
        public void ShouldRegisterServices()
        {
            // Arrange
            var services = new ServiceCollection();
            services
                .AddScoped(typeof(ILogger<>), typeof(NullLogger<>))
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(IdentityDbContext)))
                .AddDefaultEventDispatcher();

            // Act
            services.AddIdentityInfrastructure(_mockConfiguration.Object, _mockTaskifyEnvironmentSettings.Object);

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            var dbContext = serviceProvider.GetRequiredService<IdentityDbContext>();
            var repository = serviceProvider.GetRequiredService<IRepository<ContextType>>();
            var readRepository = serviceProvider.GetRequiredService<IReadRepository<ContextType>>();

            Assert.NotNull(dbContext);
            Assert.NotNull(repository);
            Assert.IsType<IdentityRepository<ContextType>>(repository);
            Assert.IsType<IdentityRepository<ContextType>>(readRepository);
        }
    }   
}

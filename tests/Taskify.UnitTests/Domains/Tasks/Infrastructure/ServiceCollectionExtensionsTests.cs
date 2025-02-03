namespace Taskify.UnitTests.Domains.Tasks.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Moq;

using Taskify.Tasks.Infrastructure;
using Taskify.Tasks.Infrastructure.Data;
using Taskify.SharedKernel;
using Taskify.SharedKernel.Data;

using Xunit;
using Taskify.Tasks.Core.ToDoItemAggregate;
using Taskify.UnitTests.TestHelpers;

public class ServiceCollectionExtensionsTests
{
    public class AddTasksInfrastructureTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration = new Mock<IConfiguration>();

        [Fact]
        public void ShouldRegisterServices()
        {
            // Arrange
            var services = new ServiceCollection();
            services
                .AddScoped(typeof(ILogger<>), typeof(NullLogger<>))
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(TasksDbContext)))
                .AddDefaultEventDispatcher();

            // Act
            services.AddTasksInfrastructure(_mockConfiguration.Object);

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            var dbContext = serviceProvider.GetRequiredService<TasksDbContext>();
            var repository = serviceProvider.GetRequiredService<IRepository<ToDoItem>>();
            var readRepository = serviceProvider.GetRequiredService<IReadRepository<ToDoItem>>();

            Assert.NotNull(dbContext);
            Assert.NotNull(repository);
            Assert.IsType<TasksRepository<ToDoItem>>(repository);
            Assert.IsType<TasksRepository<ToDoItem>>(readRepository);
        }
    }   
}

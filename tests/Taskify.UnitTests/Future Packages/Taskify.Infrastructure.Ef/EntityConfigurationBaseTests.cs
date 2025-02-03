namespace Taskify.Infrastructure.Ef.Tests;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

using Taskify.UnitTests.TestHelpers;
using Taskify.UnitTests.TestHelpers.Objects;

using Xunit;

public class EntityConfigurationBaseTests
{
    [Fact]
    public void SetupMappings_Should_Setup_Id_Property()
    {
        // Arrange
        var modelBuilder = new ModelBuilder(new ConventionSet());
        var entityBuilder = modelBuilder.Entity<TestEntity>();
        var entityConfiguration = new TestEntityConfiguration();

        // Act
        entityConfiguration.Configure(entityBuilder);

        // Assert
        var entityType = modelBuilder.Model.FindEntityType(typeof(TestEntity));
        var key = entityType?.FindPrimaryKey();

        Assert.NotNull(key);
        Assert.Single(key.Properties);
        Assert.Equal("Id", key.Properties[0].Name);
    }
}

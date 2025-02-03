namespace Taskify.Infrastructure.Ef.Tests;

using Microsoft.EntityFrameworkCore;

using Moq;

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Taskify.SharedKernel.Events;
using Taskify.UnitTests.TestHelpers;
using Taskify.UnitTests.TestHelpers.Objects;

using Xunit;

public class ReadRepositoryTests
{
    private static TestDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB per test
            .Options;

        return new TestDbContext(options, new Mock<IDomainEventDispatcher>().Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        // Seed data in an isolated scope
        using (var context = CreateDbContext())
        {
            context.TestEntities.AddRange(new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "Entity 1" },
                new TestEntity { Id = 2, Name = "Entity 2" },
                new TestEntity { Id = 3, Name = "Entity 3" }
            });
            await context.SaveChangesAsync();

            // Act
            var readRepository = new TestReadRepository(context);
            var result = await readRepository.GetAllAsync();

            // Assert
            Assert.Equal(3, result.Count); // Verify count
            Assert.Contains(result, e => e.Name == "Entity 1");
            Assert.Contains(result, e => e.Name == "Entity 2");
            Assert.Contains(result, e => e.Name == "Entity 3");
        }
    }

    [Fact]
    public async Task GetAllAsync_WithPredicate_ShouldReturnFilteredEntities()
    {
        // Arrange
        // Seed data in an isolated scope
        using (var context = CreateDbContext())
        {
            context.TestEntities.AddRange(new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "Entity 1" },
                new TestEntity { Id = 2, Name = "Test Entity 2" },
                new TestEntity { Id = 3, Name = "Test Entity 3" }
            });
            await context.SaveChangesAsync();
        
            Expression<Func<TestEntity, bool>> predicate = e => e.Name.StartsWith("Test");

            // Act
            var readRepository = new TestReadRepository(context);
            var result = await readRepository.GetAllAsync(predicate);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, e => e.Name == "Test Entity 2");
            Assert.Contains(result, e => e.Name == "Test Entity 3");
        }
    }

    [Fact]
    public async Task GetAsync_WithPredicate_ShouldReturnSingleEntity()
    {
        // Arrange
        // Seed data in an isolated scope
        using (var context = CreateDbContext())
        {
            context.TestEntities.AddRange(new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "Entity 1" },
                new TestEntity { Id = 2, Name = "Test Entity 2" },
                new TestEntity { Id = 3, Name = "Test Entity 3" }
            });
            await context.SaveChangesAsync();

            Expression<Func<TestEntity, bool>> predicate = e => e.Name.StartsWith("Test Entity 2");

            // Act
            var readRepository = new TestReadRepository(context);
            var result = await readRepository.GetAsync(predicate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Entity 2", result.Name);
        }
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnSingleEntity()
    {
        // Arrange
        // Seed data in an isolated scope
        using (var context = CreateDbContext())
        {
            context.TestEntities.AddRange(new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "Entity 1" },
                new TestEntity { Id = 2, Name = "Test Entity 2" },
                new TestEntity { Id = 3, Name = "Test Entity 3" }
            });
            await context.SaveChangesAsync();

            // Act
            var readRepository = new TestReadRepository(context);
            var result = await readRepository.GetByIdAsync(3);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Id);
            Assert.Equal("Test Entity 3", result.Name);
        }
    }
}

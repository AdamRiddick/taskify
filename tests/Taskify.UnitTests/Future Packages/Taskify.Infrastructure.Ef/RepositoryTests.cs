namespace Taskify.UnitTests.Future_Packages.Taskify.Infrastructure.Ef;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using global::Taskify.SharedKernel.Events;
using global::Taskify.UnitTests.TestHelpers;
using global::Taskify.UnitTests.TestHelpers.Objects;

using Microsoft.EntityFrameworkCore;

using Moq;

using Xunit;

public class RepositoryTests
{
    private static TestDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB per test
            .Options;

        return new TestDbContext(options, new Mock<IDomainEventDispatcher>().Object);
    }

    [Fact]
    public async Task AddAsync_ShouldAddEntity()
    {
        using var context = CreateDbContext();
        var repository = new TestRepository(context);

        var entity = new TestEntity { Name = "Test Entity" };
        var result = await repository.AddAsync(entity);

        Assert.Equal(entity, result);
        Assert.Equal(1, await context.TestEntities.CountAsync());
    }

    [Fact]
    public async Task AddRangeAsync_ShouldAddMultipleEntities()
    {
        using var context = CreateDbContext();
        var repository = new TestRepository(context);

        var entities = new List<TestEntity>
        {
            new TestEntity { Name = "Entity 1" },
            new TestEntity { Name = "Entity 2" }
        };

        await repository.AddRangeAsync(entities);

        Assert.Equal(2, await context.TestEntities.CountAsync());
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveEntity()
    {
        using var context = CreateDbContext();
        var repository = new TestRepository(context);

        var entity = new TestEntity { Name = "Test Entity" };
        await repository.AddAsync(entity);
        await repository.DeleteAsync(entity);

        Assert.Equal(0, await context.TestEntities.CountAsync());
    }

    [Fact]
    public async Task DeleteRangeAsync_ShouldRemoveMultipleEntities()
    {
        using var context = CreateDbContext();
        var repository = new TestRepository(context);

        var entities = new List<TestEntity>
        {
            new TestEntity { Name = "Entity 1" },
            new TestEntity { Name = "Entity 2" }
        };

        await repository.AddRangeAsync(entities);
        await repository.DeleteRangeAsync(entities);

        Assert.Equal(0, await context.TestEntities.CountAsync());
    }

    [Fact]
    public async Task DeleteRangeAsync_ByPredicate_ShouldRemoveMatchingEntities()
    {
        using var context = CreateDbContext();
        var repository = new TestRepository(context);

        var entities = new List<TestEntity>
        {
            new TestEntity { Name = "Keep Me" },
            new TestEntity { Name = "Delete Me" }
        };

        await repository.AddRangeAsync(entities);
        await repository.DeleteRangeAsync(e => e.Name.Contains("Delete"));

        Assert.Single(await context.TestEntities.ToListAsync());
    }

    [Fact]
    public async Task UpdateAsync_ShouldModifyEntity()
    {
        using var context = CreateDbContext();
        var repository = new TestRepository(context);

        var entity = new TestEntity { Name = "Old Name" };
        await repository.AddAsync(entity);

        entity.Name = "Updated Name";
        await repository.UpdateAsync(entity);

        var updatedEntity = await context.TestEntities.FindAsync(entity.Id);

        Assert.NotNull(updatedEntity);
        Assert.Equal("Updated Name", updatedEntity.Name);
    }

    [Fact]
    public async Task UpdateRangeAsync_ShouldModifyMultipleEntities()
    {
        using var context = CreateDbContext();
        var repository = new TestRepository(context);

        var entities = new List<TestEntity>
        {
            new TestEntity { Name = "Old Name 1" },
            new TestEntity { Name = "Old Name 2" }
        };

        await repository.AddRangeAsync(entities);

        entities[0].Name = "Updated Name 1";
        entities[1].Name = "Updated Name 2";

        await repository.UpdateRangeAsync(entities);

        var updatedEntities = await context.TestEntities.ToListAsync();
        Assert.Contains(updatedEntities, e => e.Name == "Updated Name 1");
        Assert.Contains(updatedEntities, e => e.Name == "Updated Name 2");
    }
}


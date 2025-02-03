namespace Taskify.UnitTests.TestHelpers;

using Microsoft.EntityFrameworkCore;
using Taskify.Infrastructure.Ef;
using Taskify.SharedKernel.Events;
using Taskify.UnitTests.TestHelpers.Objects;

public class TestDbContext : AppDbContextBase<TestDbContext>
{
    public TestDbContext(DbContextOptions<TestDbContext> options, IDomainEventDispatcher dispatcher)
        : base(options, dispatcher)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("test");
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<TestEntityWithDomainEvents> EntityWithDomainEvents { get; set; }

    public DbSet<TestEntity> TestEntities { get; set; }
}
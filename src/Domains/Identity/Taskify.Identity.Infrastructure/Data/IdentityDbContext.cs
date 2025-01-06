namespace Taskify.Identity.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

using Taskify.Infrastructure.Ef;
using Taskify.SharedKernel.Events;

public class IdentityDbContext : AppDbContextBase<IdentityDbContext>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options, IDomainEventDispatcher dispatcher)
            : base(options, dispatcher)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("identity");
        base.OnModelCreating(modelBuilder);
    }
}

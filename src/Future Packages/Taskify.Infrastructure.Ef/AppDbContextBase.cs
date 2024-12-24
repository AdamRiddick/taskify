namespace Taskify.Infrastructure.Ef;

using Ardalis.GuardClauses;

using Microsoft.EntityFrameworkCore;

using System.Reflection;
using System.Threading.Tasks;

using Taskify.SharedKernel.Events;

public abstract class AppDbContextBase<T>(
    DbContextOptions<T> options, IDomainEventDispatcher dispatcher) : DbContext(options) where T : DbContext
{
    private readonly IDomainEventDispatcher _dispatcher = dispatcher;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var assembly = Assembly.GetAssembly(typeof(T));
        Guard.Against.Null(assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        if (_dispatcher == null) return result;

        // dispatch events only if save was successful
        var entitiesWithEvents = ChangeTracker.Entries<HasDomainEventsBase>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();

        await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

        return result;
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}

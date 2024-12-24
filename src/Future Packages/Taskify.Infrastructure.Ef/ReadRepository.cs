namespace Taskify.Infrastructure.Ef;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Taskify.SharedKernel.Data;

public abstract class ReadRepository<T>(DbContext dbContext) : IReadRepository<T> where T : class, IEntity, IAggregateRoot
{
    protected DbContext DbContext { get; private set; } = dbContext;

    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return DbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<T>().Where(predicate).ToListAsync(cancellationToken);
    }

    public Task<T?> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }
}

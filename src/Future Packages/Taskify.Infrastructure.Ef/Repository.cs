namespace Taskify.Infrastructure.Ef;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Taskify.SharedKernel.Data;

public abstract class Repository<T>(DbContext dbContext) : ReadRepository<T>(dbContext), IRepository<T> where T : class, IEntity, IAggregateRoot
{
    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbContext.Set<T>().Add(entity);
        await SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        DbContext.Set<T>().AddRange(entities);
        await SaveChangesAsync(cancellationToken);
        return entities;
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbContext.Set<T>().Remove(entity);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        DbContext.Set<T>().RemoveRange(entities);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var query = await GetAllAsync(predicate, cancellationToken);
        DbContext.Set<T>().RemoveRange(query);
        await SaveChangesAsync(cancellationToken);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbContext.Set<T>().Update(entity);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        DbContext.Set<T>().UpdateRange(entities);
        await SaveChangesAsync(cancellationToken);
    }
}

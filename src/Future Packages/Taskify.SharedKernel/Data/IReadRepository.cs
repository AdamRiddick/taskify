namespace Taskify.SharedKernel.Data;

using System.Linq.Expressions;

public interface IReadRepository<T> where T : class, IAggregateRoot
{
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}

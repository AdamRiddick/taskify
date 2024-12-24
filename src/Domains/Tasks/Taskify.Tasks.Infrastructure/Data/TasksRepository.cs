namespace Taskify.Tasks.Infrastructure.Data
{
    using Taskify.Infrastructure.Ef;
    using Taskify.SharedKernel.Data;

    public class TasksRepository<T>(TasksDbContext dbContext) : Repository<T>(dbContext) 
        where T : class, IEntity, IAggregateRoot
    {
    }
}

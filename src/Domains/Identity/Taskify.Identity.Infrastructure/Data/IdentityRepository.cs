namespace Taskify.Identity.Infrastructure.Data
{
    using Taskify.Infrastructure.Ef;
    using Taskify.SharedKernel.Data;

    public class IdentityRepository<T>(IdentityDbContext dbContext) : Repository<T>(dbContext) 
        where T : class, IEntity, IAggregateRoot
    {
    }
}

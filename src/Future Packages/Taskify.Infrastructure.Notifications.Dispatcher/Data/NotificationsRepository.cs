namespace Taskify.Infrastructure.Notifications.Dispatcher.Data
{
    using Taskify.Infrastructure.Ef;
    using Taskify.SharedKernel.Data;

    public class NotificationsRepository<T>(NotificationsDbContext dbContext) : Repository<T>(dbContext)
        where T : class, IEntity, IAggregateRoot
    {
    }
}

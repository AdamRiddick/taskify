namespace Taskify.Infrastructure.Notifications.Dispatcher.Data
{
    using Microsoft.EntityFrameworkCore;

    using Taskify.Infrastructure.Ef;
    using Taskify.SharedKernel.Events;

    public class NotificationsDbContext : AppDbContextBase<NotificationsDbContext>
    {
        public NotificationsDbContext(
            DbContextOptions<NotificationsDbContext> options,
            IDomainEventDispatcher dispatcher)
                : base(options, dispatcher)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("notifications");
            base.OnModelCreating(modelBuilder);
        }
    }
}

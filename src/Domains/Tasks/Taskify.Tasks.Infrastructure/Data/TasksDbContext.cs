namespace Taskify.Tasks.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;

    using Taskify.Infrastructure.Ef;
    using Taskify.SharedKernel.Events;

    public class TasksDbContext : AppDbContextBase<TasksDbContext>
    {
        public TasksDbContext(DbContextOptions<TasksDbContext> options, IDomainEventDispatcher dispatcher)
                : base(options, dispatcher)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("tasks");
            base.OnModelCreating(modelBuilder);
        }
    }
}

namespace Taskify.Tasks.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Taskify.Infrastructure.Ef;
    using Taskify.Tasks.Core.ToDoItemAggregate;

    public class ToDoItemConfiguration : EntityConfigurationBase<ToDoItem>
    {
        public override void Configure(EntityTypeBuilder<ToDoItem> builder)
        {
            SetupMappings(builder);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(4000);
            
        }
    }
}

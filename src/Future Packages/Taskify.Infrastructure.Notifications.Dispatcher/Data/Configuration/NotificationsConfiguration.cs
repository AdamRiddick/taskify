namespace Taskify.Infrastructure.Notifications.Dispatcher.Data.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Taskify.Infrastructure.Ef;
using Taskify.SharedKernel.Notifications;

public class NotificationConfiguration : EntityConfigurationBase<Notification>
{
    public override void Configure(EntityTypeBuilder<Notification> builder)
    {
        SetupMappings(builder);

        builder.Property(x => x.UserId)
            .IsRequired();
        builder.Property(x => x.Message)
            .IsRequired()
            .HasMaxLength(4000);

    }
}

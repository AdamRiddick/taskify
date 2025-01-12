namespace Taskify.Identity.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Taskify.Identity.Core.UserAggregate;
    using Taskify.Infrastructure.Ef;

    public class NotificationPreferenceConfiguration : EntityConfigurationBase<NotificationPreference>
    {
        public override void Configure(EntityTypeBuilder<NotificationPreference> builder)
        {
            SetupMappings(builder);

            builder.Property(x => x.NotificationChannel)
                .IsRequired();

            builder.Property(x => x.NotificationType)
                .IsRequired();

            builder
                .HasIndex(x => new { x.UserId, x.NotificationType, x.NotificationChannel })
                .IsUnique();
        }
    }
}

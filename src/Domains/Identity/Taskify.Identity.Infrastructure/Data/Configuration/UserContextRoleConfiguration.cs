namespace Taskify.Identity.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Taskify.Identity.Core.UserContextRoleAggregate;
    using Taskify.Infrastructure.Ef;

    public class UserContextRoleConfiguration : EntityConfigurationBase<UserContextRole>
    {
        public override void Configure(EntityTypeBuilder<UserContextRole> builder)
        {
            SetupMappings(builder);

            builder.Property(x => x.ContextTypeId)
                .IsRequired();
            builder.Property(x => x.Role)
                .IsRequired();
            builder.Property(x => x.UserId)
                .IsRequired();
        }
    }
}

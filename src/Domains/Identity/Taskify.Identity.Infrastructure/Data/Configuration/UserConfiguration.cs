﻿namespace Taskify.Identity.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Taskify.Identity.Core.UserAggregate;
    using Taskify.Infrastructure.Ef;

    public class UserConfiguration : EntityConfigurationBase<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            SetupMappings(builder);

            builder.Property(x => x.EmailAddress)
                .HasMaxLength(254)
                .IsRequired();
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}

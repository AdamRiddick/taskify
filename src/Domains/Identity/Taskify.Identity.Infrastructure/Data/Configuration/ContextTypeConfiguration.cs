namespace Taskify.Identity.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Taskify.Identity.Core.ContextTypeAggregate;
    using Taskify.Infrastructure.Ef;

    public class ContextTypeConfiguration : EntityConfigurationBase<ContextType>
    {
        public override void Configure(EntityTypeBuilder<ContextType> builder)
        {
            SetupMappings(builder);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}

namespace Taskify.Infrastructure.Ef;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Taskify.SharedKernel.Data;

public abstract class EntityConfigurationBase<T> : IEntityTypeConfiguration<T> where T : class, IEntity
{
    public abstract void Configure(EntityTypeBuilder<T> builder);

    protected static void SetupMappings(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();
    }
}

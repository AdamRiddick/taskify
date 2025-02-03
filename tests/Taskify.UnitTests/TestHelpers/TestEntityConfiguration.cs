namespace Taskify.UnitTests.TestHelpers;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskify.Infrastructure.Ef;
using Taskify.UnitTests.TestHelpers.Objects;

public class TestEntityConfiguration : EntityConfigurationBase<TestEntity>
{
    public override void Configure(EntityTypeBuilder<TestEntity> builder)
    {
        SetupMappings(builder);
    }
}
namespace Taskify.UnitTests.TestHelpers.Objects;

using Taskify.SharedKernel.Data;

public class TestEntity : IEntity, IAggregateRoot
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public TestEntity()
    {

    }
}

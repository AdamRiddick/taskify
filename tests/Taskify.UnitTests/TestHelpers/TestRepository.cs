namespace Taskify.UnitTests.TestHelpers;

using Taskify.Infrastructure.Ef;
using Taskify.UnitTests.TestHelpers.Objects;

public class TestRepository : Repository<TestEntity>
{
    public TestRepository(TestDbContext dbContext) : base(dbContext) { }
}

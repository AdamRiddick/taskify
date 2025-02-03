namespace Taskify.UnitTests.TestHelpers;

using Microsoft.EntityFrameworkCore;
using Taskify.Infrastructure.Ef;
using Taskify.UnitTests.TestHelpers.Objects;

public class TestReadRepository : ReadRepository<TestEntity>
{
    public TestReadRepository(DbContext dbContext) : base(dbContext)
    {
    }
}

namespace Taskify.UnitTests.Domains.Identity.UseCases.Users.List;
using Taskify.Identity.UseCases.Users.List;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using Moq;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.SharedKernel.Data;

using Xunit;

public class ListUsersHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsListOfUsersDto()
    {
        // Arrange
        var repositoryMock = new Mock<IRepository<ContextType>>();
        var handler = new ListUsersHandler(repositoryMock.Object);
        var query = new ListUsersQuery();
        var cancellationToken = new CancellationToken();

        var contextTypes = new List<ContextType>
        {
            new ContextType { Id = 1, Name = "User" },
            new ContextType { Id = 2, Name = "Admin" }
        };

        repositoryMock.Setup(r => r.GetAllAsync(CancellationToken.None)).ReturnsAsync(contextTypes);

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Result<IEnumerable<ListUsersDto>>>(result);
        Assert.Equal(contextTypes.Count, result.Value.Count());
        Assert.Equal(contextTypes[0].Id, result.Value.First().Id);
        Assert.Equal(contextTypes[0].Name, result.Value.First().Name);
        Assert.Equal(contextTypes[1].Id, result.Value.Last().Id);
        Assert.Equal(contextTypes[1].Name, result.Value.Last().Name);
    }
}

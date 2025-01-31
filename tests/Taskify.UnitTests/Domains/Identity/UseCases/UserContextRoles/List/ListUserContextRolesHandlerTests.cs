namespace Taskify.Identity.UseCases.UserContextRoles.List.Tests;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using Moq;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.SharedKernel.Data;

using Xunit;

public class ListUserContextRolesHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnListOfUserContextRolesDto()
    {
        // Arrange
        var contextTypes = new List<ContextType>
        {
            new ContextType { Id = 1, Name = "ContextType1" },
            new ContextType { Id = 2, Name = "ContextType2" },
            new ContextType { Id = 3, Name = "ContextType3" }
        };

        var repositoryMock = new Mock<IRepository<ContextType>>();
        repositoryMock.Setup(r => r.GetAllAsync(CancellationToken.None)).ReturnsAsync(contextTypes);

        var handler = new ListUserContextRolesHandler(repositoryMock.Object);

        var query = new ListUserContextRolesQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Result<IEnumerable<ListUserContextRolesDto>>>(result);
        Assert.Equal(contextTypes.Count, result.Value.Count());
    }
}

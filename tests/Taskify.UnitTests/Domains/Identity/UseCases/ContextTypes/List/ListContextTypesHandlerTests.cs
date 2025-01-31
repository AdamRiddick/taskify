namespace Taskify.Identity.UseCases.Tests.ContextTypes.List;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using Moq;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.Identity.UseCases.ContextTypes.List;
using Taskify.SharedKernel.Data;

using Xunit;

public class ListContextTypesHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnListOfContextTypeDtos()
    {
        // Arrange
        var contextTypes = new List<ContextType>
        {
            new ContextType { Id = 1, Name = "ContextType 1" },
            new ContextType { Id = 2, Name = "ContextType 2" },
            new ContextType { Id = 3, Name = "ContextType 3" }
        };

        var repositoryMock = new Mock<IRepository<ContextType>>();
        repositoryMock.Setup(x => x.GetAllAsync(CancellationToken.None)).ReturnsAsync(contextTypes);

        var handler = new ListContextTypesHandler(repositoryMock.Object);
        var query = new ListContextTypesQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsType<Result<IEnumerable<ListContextTypeDto>>>(result);
        Assert.Equal(3, result.Value.Count());
        Assert.Equal(1, result.Value.First().Id);
        Assert.Equal("ContextType 1", result.Value.First().Name);
        Assert.Equal(3, result.Value.Last().Id);
        Assert.Equal("ContextType 3", result.Value.Last().Name);
    }
}

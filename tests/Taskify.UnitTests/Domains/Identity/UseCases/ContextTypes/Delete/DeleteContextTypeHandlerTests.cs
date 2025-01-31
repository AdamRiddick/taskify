namespace Taskify.UnitTests.Domains.Identity.UseCases.ContextTypes.Delete;

using Ardalis.Result;

using Moq;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.Identity.Core.ContextTypeAggregate.Events;
using Taskify.Identity.UseCases.ContextTypes.Delete;
using Taskify.SharedKernel.Data;

using Xunit;

public class DeleteContextTypeHandlerTests
{
    public class HandleTests
    {
        [Fact]
        public async Task WhenItemExists_DeletesItem()
        {
            // Arrange
            var item = new ContextType();
            var repository = new Mock<IRepository<ContextType>>();
            repository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(item);
            var handler = new DeleteContextTypeHandler(repository.Object);
            var command = new DeleteContextTypeCommand(1);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            repository.Verify(x => x.DeleteAsync(item, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task WhenItemExists_MarksDeleted()
        {
            // Arrange
            var item = new ContextType();
            var repository = new Mock<IRepository<ContextType>>();
            repository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(item);
            var handler = new DeleteContextTypeHandler(repository.Object);
            var command = new DeleteContextTypeCommand(1);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Single(item.DomainEvents);
            Assert.IsType<ContextTypeDeletedEvent>(item.DomainEvents.First());
        }

        [Fact]
        public async Task WhenItemExists_ReturnsSuccess()
        {
            // Arrange
            var item = new ContextType();
            var repository = new Mock<IRepository<ContextType>>();
            repository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(item);
            var handler = new DeleteContextTypeHandler(repository.Object);
            var command = new DeleteContextTypeCommand(1);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task WhenItemDoesNotExist_DoesNotDeleteItem()
        {
            // Arrange
            var repository = new Mock<IRepository<ContextType>>();
            repository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);
            var handler = new DeleteContextTypeHandler(repository.Object);
            var command = new DeleteContextTypeCommand(1);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(ResultStatus.NotFound, result.Status);
            repository.Verify(x => x.DeleteAsync(It.IsAny<ContextType>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}

namespace Taskify.UnitTests.Domains.Identity.UseCases.ContextTypes.Create;

using Moq;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.Identity.UseCases.ContextTypes.Create;
using Taskify.SharedKernel.Data;

using Xunit;

public class CreateContextTypeHandlerTests
{
    public class HandleTests
    {
        [Fact]
        public async Task ShouldCreateContextType()
        {
            // Arrange
            var contextTypeRepository = new Mock<IRepository<ContextType>>();
            var handler = new CreateContextTypeHandler(contextTypeRepository.Object);
            var dto = new CreateContextTypeDto
            {
                Name = "Test"
            };
            var command = new CreateContextTypeCommand(dto);

            contextTypeRepository.Setup(x => x.AddAsync(It.IsAny<ContextType>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ContextType { Id = 1 });

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Value);
        }
    }
}

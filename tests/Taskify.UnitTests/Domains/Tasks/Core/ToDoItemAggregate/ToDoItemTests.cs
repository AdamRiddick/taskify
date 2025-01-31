namespace Taskify.Tasks.Core.Tests;

using Taskify.Tasks.Core.ToDoItemAggregate;
using Taskify.Tasks.Core.ToDoItemAggregate.Events;

using Xunit;

public class ToDoItemTests
{
    [Fact]
    public void MarkComplete_ShouldSetIsCompleteToTrue()
    {
        // Arrange
        var toDoItem = new ToDoItem();

        // Act
        toDoItem.MarkComplete();

        // Assert
        Assert.True(toDoItem.IsComplete);
    }

    [Fact]
    public void MarkComplete_ShouldRaiseToDoItemCompletedEvent()
    {
        // Arrange
        var toDoItem = new ToDoItem();

        // Act
        toDoItem.MarkComplete();

        // Assert
        Assert.Single(toDoItem.DomainEvents);
        Assert.IsType<ToDoItemCompletedEvent>(toDoItem.DomainEvents.First());
    }

    [Fact]
    public void MarkNew_ShouldRaiseToDoItemCreatedEvent()
    {
        // Arrange
        var toDoItem = new ToDoItem();

        // Act
        toDoItem.MarkNew();

        // Assert
        Assert.Single(toDoItem.DomainEvents);
        Assert.IsType<ToDoItemCreatedEvent>(toDoItem.DomainEvents.First());
    }
}

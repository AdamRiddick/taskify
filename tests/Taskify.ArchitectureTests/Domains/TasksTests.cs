namespace Taskify.ArchitectureTests.Domains;

using System.Reflection;

using Taskify.ArchitectureTests.Common;
using Taskify.Tasks.Core.ToDoItemAggregate;
using Taskify.Tasks.Infrastructure.Data;
using Taskify.Tasks.UseCases.ToDoItems.Create;

public class TasksTests : DomainTestsBase<TasksTests>
{
    static TasksTests()
    {
        var assembliesToCheck = new Assembly[]
        {
            typeof(ToDoItem).Assembly, // Taskify.Tasks.Core
            typeof(TasksDbContext).Assembly, // Taskify.Tasks.Infrastructure
            typeof(CreateToDoItemCommand).Assembly  // Taskify.Tasks.UseCases
        };

        Init("Tasks", assembliesToCheck);
    }
}

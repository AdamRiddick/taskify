namespace Taskify.SharedKernel.Security;

public static class SecurityContexts
{
    public static class Identity
    {
        public const string UserContextRoles = "UserContextRoles";

        public const string Users = "Users";
    }

    public static class Tasks
    {
        public const string ToDoItem = "ToDoItem";
    }
}

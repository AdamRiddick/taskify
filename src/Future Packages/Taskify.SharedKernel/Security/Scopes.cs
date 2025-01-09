namespace Taskify.SharedKernel.Security;

public static class Scopes
{
    public static class Identity
    {
        public static class ContextTypes
        {
            public const string All = "ContextTypes.All";

            public const string Read = "ContextTypes.Read";

            public const string Write = "ContextTypes.Write";
        }

        public static class UserContextRoles
        {
            public const string All = "UserContextRoles.All";

            public const string Read = "UserContextRoles.Read";

            public const string Write = "UserContextRoles.Write";
        }

        public static class Users
        {
            public const string All = "Users.All";

            public const string Read = "Users.Read";

            public const string Write = "Users.Write";
        }
    }

    public static class Tasks
    {
        public static class ToDoItem
        {
            public const string All = "ToDoItems.All";

            public const string Read = "ToDoItems.Read";

            public const string Write = "ToDoItems.Write";
        }
    }
}

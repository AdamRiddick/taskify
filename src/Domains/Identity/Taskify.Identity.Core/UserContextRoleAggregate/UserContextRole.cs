namespace Taskify.Identity.Core.UserContextRoleAggregate
{
    using Taskify.SharedKernel.Data;
    using Taskify.SharedKernel.Security;

    public class UserContextRole : EntityBase, IAggregateRoot
    {
        public int ContextTypeId { get; set; }

        public Role Role { get; set; }

        public int UserId { get; set; }
    }
}

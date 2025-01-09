namespace Taskify.Identity.Core.UserContextRoleAggregate
{
    using Taskify.Identity.Core.ContextTypeAggregate;
    using Taskify.SharedKernel.Data;
    using Taskify.SharedKernel.Security;

    public class UserContextRole : EntityBase, IAggregateRoot
    {
        public int? ContextId { get; set; }

        public int ContextTypeId { get; set; }

        public ContextType ContextType { get; set; } = new ContextType();

        public Role Role { get; set; }

        public int UserId { get; set; }
    }
}

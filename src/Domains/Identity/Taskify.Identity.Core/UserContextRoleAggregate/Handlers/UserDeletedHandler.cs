namespace Taskify.Identity.Core.UserContextRoleAggregate.Handlers;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

using Taskify.Identity.Core.UserAggregate.Events;
using Taskify.Identity.Core.UserContextRoleAggregate;
using Taskify.SharedKernel.Data;

public class UserDeletedHandler : INotificationHandler<UserDeletedEvent>
{
    private readonly IRepository<UserContextRole> _repository;

    public UserDeletedHandler(
        IRepository<UserContextRole> repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        UserDeletedEvent notification,
        CancellationToken cancellationToken)
    {
        await _repository.DeleteRangeAsync(
            x => x.ContextTypeId == notification.Id,
            cancellationToken: cancellationToken);
    }
}

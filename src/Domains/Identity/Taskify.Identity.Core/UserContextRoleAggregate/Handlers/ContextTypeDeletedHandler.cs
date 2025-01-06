namespace Taskify.Identity.Core.UserContextRoleAggregate.Handlers;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

using Taskify.Identity.Core.ContextTypeAggregate.Events;
using Taskify.Identity.Core.UserContextRoleAggregate;
using Taskify.SharedKernel.Data;

public class ContextTypeDeletedHandler : INotificationHandler<ContextTypeDeletedEvent>
{
    private readonly IRepository<UserContextRole> _repository;

    public ContextTypeDeletedHandler(
        IRepository<UserContextRole> repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        ContextTypeDeletedEvent notification,
        CancellationToken cancellationToken)
    {
        await _repository.DeleteRangeAsync(
            x => x.ContextTypeId == notification.Id,
            cancellationToken: cancellationToken);
    }
}

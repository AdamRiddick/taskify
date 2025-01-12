namespace Taskify.Infrastructure.Notifications.Dispatcher;

using System.Threading;
using System.Threading.Tasks;

using Taskify.SharedKernel.Cqrs;
using Taskify.SharedKernel.Data;
using Taskify.SharedKernel.Notifications;

public class SendNotificationHandler : ICommandHandler<SendNotificationCommand, bool>
{
    private readonly IRepository<Notification> _repository;

    public SendNotificationHandler(
        IRepository<Notification> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        SendNotificationCommand request,
        CancellationToken cancellationToken)
    {
        var createdItem = await _repository.AddAsync(request.Dto, cancellationToken);
        return createdItem != null;
    }
}

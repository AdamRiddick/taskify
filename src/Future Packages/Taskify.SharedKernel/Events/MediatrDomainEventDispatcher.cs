namespace Taskify.SharedKernel.Events;

using MediatR;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading.Tasks;

public class MediatrDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    private readonly ILogger _logger;

    public MediatrDomainEventDispatcher(
        IMediator mediator,
        ILogger<MediatrDomainEventDispatcher> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task DispatchAndClearEvents(IEnumerable<IHasDomainEvents> entitiesWithEvents)
    {
        foreach (IHasDomainEvents entity in entitiesWithEvents)
        {
            if (entity is HasDomainEventsBase hasDomainEvents)
            {
                DomainEventBase[] events = hasDomainEvents.DomainEvents.ToArray();
                hasDomainEvents.ClearDomainEvents();

                foreach (DomainEventBase domainEvent in events)
                    await _mediator.Publish(domainEvent).ConfigureAwait(false);
            }
            else
            {
                _logger.LogError(
                  "Entity of type {EntityType} does not inherit from {BaseType}. Unable to clear domain events.",
                  entity.GetType().Name,
                  nameof(HasDomainEventsBase));
            }
        }
    }
}

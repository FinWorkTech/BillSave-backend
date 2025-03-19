using BillSave.API.Sales.Application.ACL.IntegrationEvents;
using BillSave.API.Sales.Domain.Model.Events;
using MediatR;

namespace BillSave.API.Sales.Application.Internal.EventHandlers;

/// <summary>
/// This event handler listens to the document changed events and publishes the corresponding integration events.
/// </summary>
/// <param name="mediator">
/// The mediator to publish the integration events.
/// </param>
public class DocumentChangedEventToIntegrationEventHandler
    (
        IMediator mediator
    )
    : INotificationHandler<DocumentCreatedEvent>,
      INotificationHandler<DocumentUpdatedEvent>,
      INotificationHandler<DocumentDeletedEvent>
{
    public async Task Handle(DocumentCreatedEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new DocumentCreatedIntegrationEvent(notification.PackId);
        await mediator.Publish(integrationEvent, cancellationToken);
    }

    public Task Handle(DocumentUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new DocumentUpdatedIntegrationEvent(notification.PackId);
        return mediator.Publish(integrationEvent, cancellationToken);
    }

    public Task Handle(DocumentDeletedEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new DocumentDeletedIntegrationEvent(notification.PackId);
        return mediator.Publish(integrationEvent, cancellationToken);
    }
}
using MediatR;
using BillSave.API.Sales.Domain.Model.Events;
using BillSave.API.Portfolio.Application.Interfaces.CommandServices;
using BillSave.API.Portfolio.Domain.Model.Commands;

namespace BillSave.API.Portfolio.Application.Internal.EventHandlers;

public class DocumentChangedEventHandler
    (
        IPackCommandService packCommandService
    ) 
    : INotificationHandler<DocumentCreatedEvent>, 
      INotificationHandler<DocumentUpdatedEvent>,
      INotificationHandler<DocumentDeletedEvent>
{
    public async Task Handle(DocumentCreatedEvent notification, CancellationToken cancellationToken)
    {
        var updateEffectiveAnnualCostRateCommand = new UpdateEffectiveAnnualCostRateCommand(notification.PackId);
        await packCommandService.Handle(updateEffectiveAnnualCostRateCommand);
        
        var updateQuantityOfDocumentsCommand = new UpdateQuantityOfDocumentsCommand(notification.PackId, "increment");
        await packCommandService.Handle(updateQuantityOfDocumentsCommand);
    }

    public async Task Handle(DocumentUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var command = new UpdateEffectiveAnnualCostRateCommand(notification.PackId);
        await packCommandService.Handle(command);
    }

    public Task Handle(DocumentDeletedEvent notification, CancellationToken cancellationToken)
    {
        var updateQuantityOfDocumentsCommand = new UpdateQuantityOfDocumentsCommand(notification.PackId, "decrement");
        return packCommandService.Handle(updateQuantityOfDocumentsCommand);
    }
}
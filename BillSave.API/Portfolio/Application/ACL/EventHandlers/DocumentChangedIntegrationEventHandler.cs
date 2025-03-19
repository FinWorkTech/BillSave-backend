using MediatR;
using BillSave.API.Portfolio.Domain.Model.Commands;
using BillSave.API.Sales.Application.ACL.IntegrationEvents;
using BillSave.API.Portfolio.Application.Interfaces.CommandServices;

namespace BillSave.API.Portfolio.Application.ACL.EventHandlers;

/// <summary>
/// This class is responsible for handling the integration events related to the document entity.
/// </summary>
/// <param name="packCommandService">
/// The service that will handle the commands related to the pack entity.
/// </param>
public class DocumentChangedIntegrationEventHandler
    (
        IPackCommandService packCommandService
    ) 
    : INotificationHandler<DocumentCreatedIntegrationEvent>, 
      INotificationHandler<DocumentUpdatedIntegrationEvent>,
      INotificationHandler<DocumentDeletedIntegrationEvent>
{
    public async Task Handle(DocumentCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var updateEffectiveAnnualCostRateCommand = new UpdateEffectiveAnnualCostRateCommand(notification.PackId);
        await packCommandService.Handle(updateEffectiveAnnualCostRateCommand);
        
        var updateQuantityOfDocumentsCommand = new UpdateQuantityOfDocumentsCommand(notification.PackId, "increment");
        await packCommandService.Handle(updateQuantityOfDocumentsCommand);
    }

    public async Task Handle(DocumentUpdatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var command = new UpdateEffectiveAnnualCostRateCommand(notification.PackId);
        await packCommandService.Handle(command);
    }

    public async Task Handle(DocumentDeletedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var updateEffectiveAnnualCostRateCommand = new UpdateEffectiveAnnualCostRateCommand(notification.PackId);
        await packCommandService.Handle(updateEffectiveAnnualCostRateCommand);
        
        var updateQuantityOfDocumentsCommand = new UpdateQuantityOfDocumentsCommand(notification.PackId, "decrement");
        await packCommandService.Handle(updateQuantityOfDocumentsCommand);
    }
}
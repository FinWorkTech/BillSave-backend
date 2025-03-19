using MediatR;

namespace BillSave.API.Sales.Application.ACL.IntegrationEvents;

/// <summary>
/// This event is used to notify the other services that a document has been deleted.
/// </summary>
/// <param name="PackId">
/// The PackId of the document that has been deleted.
/// </param>
public record DocumentDeletedIntegrationEvent(int PackId) : INotification;
using MediatR;

namespace BillSave.API.Sales.Application.ACL.IntegrationEvents;

/// <summary>
/// This event is used to notify the other services that a document has been created.
/// </summary>
/// <param name="PackId">
/// The PackId of the document that has been created.
/// </param>
public record DocumentCreatedIntegrationEvent(int PackId) : INotification;
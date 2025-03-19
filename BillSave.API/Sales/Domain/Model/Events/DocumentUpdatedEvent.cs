using MediatR;

namespace BillSave.API.Sales.Domain.Model.Events;

/// <summary>
/// The Document Updated Event is used to notify the system that a Document has been updated.
/// </summary>
/// <param name="PackId">
/// The PackId of the Document that has been updated.
/// </param>
public record DocumentUpdatedEvent(int PackId) : INotification;
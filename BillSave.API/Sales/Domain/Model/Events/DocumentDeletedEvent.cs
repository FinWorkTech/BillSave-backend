using MediatR;

namespace BillSave.API.Sales.Domain.Model.Events;

/// <summary>
/// The Document Deleted Event is raised when a document is deleted.
/// </summary>
/// <param name="PackId">
/// The PackId of the document that was deleted.
/// </param>
public record DocumentDeletedEvent(int PackId) : INotification;
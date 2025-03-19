using MediatR;

namespace BillSave.API.Sales.Domain.Model.Events;

/// <summary>
/// The Document Created Event represents the event that is raised when a Document is created.
/// </summary>
/// <param name="PackId">
/// The PackId of the Document that was created.
/// </param>
public record DocumentCreatedEvent(int PackId) : INotification;
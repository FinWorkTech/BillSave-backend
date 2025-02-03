using MediatR;

namespace BillSave.API.Sales.Domain.Model.Events;

/// Document Changed Event.
/// <summary>
/// This class represents the document changed event. It is used to notify that a document has changed.
/// </summary>
/// <param name="PortfolioId">
/// The portfolio id.
/// </param>
public record DocumentChangedEvent(int PortfolioId) : INotification;
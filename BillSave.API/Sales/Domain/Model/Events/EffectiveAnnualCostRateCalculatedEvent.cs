using MediatR;

namespace BillSave.API.Sales.Domain.Model.Events;

/// Effective Annual Cost Rate Calculated Event.
/// <summary>
/// This class represents the effective annual cost rate calculated event. It is used to notify that the effective annual cost rate has been calculated.
/// </summary>
/// <param name="DocumentId">
/// The document id.
/// </param>
public record EffectiveAnnualCostRateCalculatedEvent(int DocumentId) : INotification;
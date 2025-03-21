using BillSave.API.Portfolio.Domain.Model.Commands;
using BillSave.API.Portfolio.Domain.Model.ValueObjects;
using BillSave.API.Shared.Domain.Model.ValueObjects;

namespace BillSave.API.Portfolio.Domain.Model.Aggregates;

/// Portfolio aggregate root.
/// <summary>
/// This class represents the Portfolio aggregate root. It is used to encapsulate the business logic of the Portfolio entity.
/// </summary>
public partial class Pack
{
    public int Id { get; }
    public int UserId { get; }
    public string Name { get; private set; }
    public SimpleDate DiscountDate { get; private set; }
    
    public int TotalDocuments { get; private set; }
    public EffectiveAnnualCostRate EffectiveAnnualCostRate { get; private set; }

    public Pack()
    {
        Name = string.Empty;
        DiscountDate = new SimpleDate(DateTime.MinValue);
        TotalDocuments = 0;
        EffectiveAnnualCostRate = new EffectiveAnnualCostRate(0);
    }
    
    public Pack(string name, DateTime discountDate, int totalDocuments, EffectiveAnnualCostRate effectiveAnnualCostRate)
    {
        Name = name;
        DiscountDate = new SimpleDate(discountDate);
        TotalDocuments = totalDocuments;
        EffectiveAnnualCostRate = effectiveAnnualCostRate;
    }

    /// <summary>
    /// Constructor for creating a new Portfolio.
    /// </summary>
    /// <param name="command">
    /// The <see cref="CreatePackCommand"/> command
    /// </param>
    /// <param name="userId">
    /// The user id of the Portfolio.
    /// </param>
    public Pack(CreatePackCommand command, int userId)
    {
        UserId = userId;
        Name = command.Name;
        DiscountDate = new SimpleDate(command.DiscountDate);
        
        TotalDocuments = 0;
        EffectiveAnnualCostRate = new EffectiveAnnualCostRate(0);
    }

    public void UpdatePack(UpdatePackCommand command)
    {
        Name = command.Name;
        DiscountDate = new SimpleDate(command.DiscountDate);
    }
    
    public void UpdateTotalDocuments(int totalDocuments)
    {
        TotalDocuments = totalDocuments;
    }
    
    public void UpdateEffectiveAnnualCostRate(decimal effectiveAnnualCostRate)
    {
        EffectiveAnnualCostRate = new EffectiveAnnualCostRate(effectiveAnnualCostRate);
    }
}
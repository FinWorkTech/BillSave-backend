using BillSave.API.Portfolio.Domain.Model.Commands;
using BillSave.API.Portfolio.Domain.Model.ValueObjects;

namespace BillSave.API.Portfolio.Domain.Model.Aggregates;

/// Portfolio aggregate root.
/// <summary>
/// This class represents the Portfolio aggregate root. It is used to encapsulate the business logic of the Portfolio entity.
/// </summary>
public partial class Pack
{
    public int Id { get; }
    public string Name { get; private set; }
    public DateTime DiscountDate { get; private set; }
    
    public int TotalDocuments { get; private set; }
    public EffectiveAnnualCostRate EffectiveAnnualCostRate { get; private set; }

    public Pack()
    {
        Name = string.Empty;
        DiscountDate = DateTime.MinValue;
        TotalDocuments = 0;
        EffectiveAnnualCostRate = new EffectiveAnnualCostRate(0);
    }
    
    public Pack(string name, DateTime discountDate, int totalDocuments, EffectiveAnnualCostRate effectiveAnnualCostRate)
    {
        Name = name;
        DiscountDate = discountDate;
        TotalDocuments = totalDocuments;
        EffectiveAnnualCostRate = effectiveAnnualCostRate;
    }
    
    /// <summary>
    /// Constructor for creating a new Portfolio.
    /// </summary>
    /// <param name="command">
    /// The <see cref="CreatePackCommand"/> command
    /// </param>
    public Pack(CreatePackCommand command)
    {
        Name = command.Name;
        DiscountDate = command.DiscountDate;
        
        TotalDocuments = 0;
        EffectiveAnnualCostRate = new EffectiveAnnualCostRate(0);
    }
}
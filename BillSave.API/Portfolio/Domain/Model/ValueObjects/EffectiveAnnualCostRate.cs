namespace BillSave.API.Portfolio.Domain.Model.ValueObjects;

/// <summary>
/// The effective annual cost rate (TCEA) is the annualized cost of a financial product, including all costs and expenses.
/// </summary>
public record EffectiveAnnualCostRate
{
    private decimal Value { get; }

    public EffectiveAnnualCostRate(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("The effective annual cost rate cannot be negative.");

        Value = value;
    }
    
    public override string ToString()
    {
        return $"{Value}%";
    }
}
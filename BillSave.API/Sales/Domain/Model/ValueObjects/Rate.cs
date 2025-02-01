namespace BillSave.API.Sales.Domain.Model.ValueObjects;

/// <summary>
/// Value Object representing an interest rate.
/// </summary>
public record Rate
{
    public decimal Value { get; }
    public string Type { get; } 

    public Rate(decimal value, string type)
    {
        if (value <= 0)
            throw new ArgumentException("The rate must be greater than 0.");

        if (type != "Nominal" && type != "Effective")
            throw new ArgumentException("The rate type must be either 'Nominal' or 'Effective'.");

        Value = value;
        Type = type;
    }

    public override string ToString() => $"{Value} ({Type})";
}
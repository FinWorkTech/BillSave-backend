namespace BillSave.API.Sales.Domain.Model.ValueObjects;

/// <summary>
/// Value Object representing the nominal value of a document.
/// </summary>
public record NominalValue
{
    public decimal Value { get; }

    public NominalValue(decimal value)
    {
        if (value <= 0)
            throw new ArgumentException("Nominal value must be greater than 0.");

        Value = value;
    }

    public override string ToString() => Value.ToString("F2");
}
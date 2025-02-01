namespace BillSave.API.Sales.Domain.Model.ValueObjects;

/// <summary>
/// Value Object representing a currency (PEN or USD).
/// </summary>
public record Currency
{
    public string Code { get; }

    public Currency(string code)
    {
        if (code != "PEN" && code != "USD")
            throw new ArgumentException("Currency must be 'PEN' or 'USD'.");

        Code = code;
    }

    public override string ToString() => Code;
}
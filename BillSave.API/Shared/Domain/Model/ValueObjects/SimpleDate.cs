namespace BillSave.API.Shared.Domain.Model.ValueObjects;

/// Simple date value object
/// <summary>
/// This class represents the simple date value object. It is used to store the simple date.
/// </summary>
public record SimpleDate()
{
    public DateTime Value { get; }
    
    public SimpleDate(DateTime value) : this()
    {
        Value = value.Date;
    }
    
    public override string ToString() => Value.ToString("yyyy-MM-dd");
}
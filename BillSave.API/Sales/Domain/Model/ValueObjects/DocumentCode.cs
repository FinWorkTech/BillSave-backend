namespace BillSave.API.Sales.Domain.Model.ValueObjects;

/// Document Code
/// <summary>
/// This class represents the document code value object.
/// </summary>
public record DocumentCode
{
    public string Value { get; }

    public DocumentCode(string value = "Empty")
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Document code cannot be empty", nameof(value));

        if (value.Length > 50)
            throw new ArgumentException("Code cannot be longer than 50 characters.");
        
        Value = value;
    }
    
    public override string ToString() => Value;
}
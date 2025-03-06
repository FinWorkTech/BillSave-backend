namespace BillSave.API.Shared.Domain.Model.ValueObjects;

public record ExchangeRate
{
    public DateOnly Date { get; }
    public string Currency { get; }
    public decimal BuyPrice { get; }
    public decimal SellPrice { get; }
    
    public ExchangeRate(DateOnly date, string currency, decimal buyPrice, decimal sellPrice)
    {
        if (buyPrice <= 0 || sellPrice <= 0)
            throw new ArgumentException("Exchange rates must be positive values.");
        
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency cannot be empty.");
        
        Date = date;
        Currency = currency;
        BuyPrice = buyPrice;
        SellPrice = sellPrice;
    }
}
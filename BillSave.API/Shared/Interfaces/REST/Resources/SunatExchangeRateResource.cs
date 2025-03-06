namespace BillSave.API.Shared.Interfaces.REST.Resources;

public record SunatExchangeRateResource(
    decimal BuyPrice, 
    decimal SellPrice, 
    string Currency, 
    DateOnly Date
    );
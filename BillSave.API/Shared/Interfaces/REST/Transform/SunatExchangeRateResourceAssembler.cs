using BillSave.API.Shared.Domain.Model.ValueObjects;
using BillSave.API.Shared.Interfaces.REST.Resources;

namespace BillSave.API.Shared.Interfaces.REST.Transform;

public class SunatExchangeRateResourceAssembler
{
    public static SunatExchangeRateResource ToResource(ExchangeRate exchangeRate)
    {
        return new SunatExchangeRateResource(
            exchangeRate.BuyPrice,
            exchangeRate.SellPrice,
            exchangeRate.Currency,
            exchangeRate.Date
        );
    }
}
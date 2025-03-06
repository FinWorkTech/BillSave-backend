using BillSave.API.Shared.Domain.Model.ValueObjects;

namespace BillSave.API.Shared.Domain.Services;

public interface ISunatService
{
    Task<ExchangeRate> GetExchangeRateAsync(DateOnly date);
}
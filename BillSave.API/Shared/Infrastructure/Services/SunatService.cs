using BillSave.API.Shared.Domain.Model.ValueObjects;
using BillSave.API.Shared.Domain.Services;

namespace BillSave.API.Shared.Infrastructure.Services;

public class SunatService : ISunatService
{
    private readonly HttpClient _httpClient;
    
    private readonly string _baseUrl;
    private readonly string _apiToken;

    public SunatService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        
        _baseUrl = configuration["ExternalApis:SunatBaseUrl"] ??
                   throw new ArgumentNullException("SunatBaseUrl is not configured.");
        _apiToken = configuration["ExternalApis:SunatApiToken"] ??
                    throw new ArgumentNullException("SunatApiToken is not configured.");
    }

    public async Task<ExchangeRate> GetExchangeRateAsync(DateOnly date)
    {
        string url = $"{_baseUrl}/v2/sunat/tipo-cambio?date={date:yyyy-MM-dd}";

        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("Authorization", $"Bearer {_apiToken}");

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseData = await response.Content.ReadFromJsonAsync<SunatApiResponse>();

        if (responseData == null)
            throw new Exception("No response data from Sunat API");

        return new ExchangeRate(
            DateOnly.Parse(responseData.Fecha),
            responseData.Moneda,
            responseData.PrecioCompra,
            responseData.PrecioVenta
            );
    }
}

public class SunatApiResponse
{
    public decimal PrecioCompra { get; set; }
    public decimal PrecioVenta { get; set; }
    public string Moneda { get; set; }
    public string Fecha { get; set; }
}
using System.Net.Mime;
using BillSave.API.Shared.Domain.Services;
using BillSave.API.Shared.Interfaces.REST.Resources;
using BillSave.API.Shared.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BillSave.API.Shared.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Sunat")]
public class SunatController(ISunatService sunatService) : ControllerBase
{
    [HttpGet("exchange-rate/{date}")]
    [SwaggerOperation(
        Summary = "Get exchange rate by date",
        Description = "Retrieve the exchange rate for a specific date from SUNAT",
        OperationId = "GetExchangeRateByDate")]
    [SwaggerResponse(StatusCodes.Status200OK, 
        "The exchange rate was retrieved successfully", typeof(SunatExchangeRateResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid date format. Use YYYY-MM-DD.")]
    public async Task<ActionResult> GetExchangeRate(DateOnly date)
    {
        var exchangeRate = await sunatService.GetExchangeRateAsync(date);

        return Ok(SunatExchangeRateResourceAssembler.ToResource(exchangeRate));
    }
}
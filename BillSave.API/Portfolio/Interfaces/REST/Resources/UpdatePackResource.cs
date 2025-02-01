namespace BillSave.API.Portfolio.Interfaces.REST.Resources;

/// <summary>
/// The update pack resource.
/// </summary>
/// <param name="Id">
/// The pack identifier.
/// </param>
/// <param name="Name">
/// The pack name.
/// </param>
/// <param name="DiscountDate">
/// The pack discount date.
/// </param>
public record UpdatePackResource(int Id, string Name, DateTime DiscountDate);
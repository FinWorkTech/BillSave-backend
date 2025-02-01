namespace BillSave.API.Portfolio.Interfaces.REST.Resources;

/// <summary>
/// The create pack resource.
/// </summary>
/// <param name="Name">
/// The name of the pack.
/// </param>
/// <param name="DiscountDate">
/// The discount date of the pack.
/// </param>
/// <param name="UserId">
/// The user id of the pack.
/// </param>
public record CreatePackResource(string Name, DateTime DiscountDate, int UserId);
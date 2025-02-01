namespace BillSave.API.Portfolio.Interfaces.REST.Resources;

/// <summary>
/// The pack resource.
/// </summary>
/// <param name="Id">
/// The id of the pack.
/// </param>
/// <param name="UserId">
/// The user id of the pack.
/// </param>
/// <param name="Name">
/// The name of the pack.
/// </param>
/// <param name="DiscountDate">
/// The discount date of the pack.
/// </param>
public record PackResource(int Id, int UserId, string Name, DateTime DiscountDate);
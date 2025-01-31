namespace BillSave.API.IAM.Domain.Model.Queries;

/// <summary>
/// Get user by id query
/// </summary>
/// <param name="Id">
/// The user id
/// </param>
public record GetUserByIdQuery(int Id);
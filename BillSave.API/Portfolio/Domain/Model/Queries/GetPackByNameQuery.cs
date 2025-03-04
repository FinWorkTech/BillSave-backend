namespace BillSave.API.Portfolio.Domain.Model.Queries;

/// Query to get a pack by name
/// <summary>
/// This query is used to get a pack by name
/// </summary>
/// <param name="Name">
/// The name of the pack to get
/// </param>
public record GetPackByNameQuery(string Name);
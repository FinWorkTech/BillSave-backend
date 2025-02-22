namespace BillSave.API.Sales.Domain.Model.Queries;

/// Get Document By Id Query
/// <summary>
/// This class is used to get document by Id
/// </summary>
/// <param name="Id">
/// The Id of the document
/// </param>
public record GetDocumentByIdQuery(int Id);
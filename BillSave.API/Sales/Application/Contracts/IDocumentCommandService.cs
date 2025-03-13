using BillSave.API.Sales.Domain.Model.Aggregates;
using BillSave.API.Sales.Domain.Model.Commands;

namespace BillSave.API.Sales.Application.Contracts;

/// Document Command Service Interface
/// <summary>
/// This interface defines the methods that the Document Command Service must implement.
/// </summary>
public interface IDocumentCommandService
{
    /// <summary>
    /// This method is used to get a document by its id.
    /// </summary>
    /// <param name="command">
    /// The <see cref="CreateDocumentCommand"/> command.
    /// </param>
    /// <returns>
    /// The <see cref="Document"/> object.
    /// </returns>
    Task<Document?> Handle(CreateDocumentCommand command);
    
    /// <summary>
    /// This method is used to get a document by its id.
    /// </summary>
    /// <param name="command">
    /// The <see cref="UpdateDocumentCommand"/> command.
    /// </param>
    /// <returns>
    /// The <see cref="Document"/> object.
    /// </returns>
    Task<Document?> Handle(UpdateDocumentCommand command);
    
    /// <summary>
    /// This method is used to get a document by its id.
    /// </summary>
    /// <param name="command">
    /// The <see cref="UpdateEffectiveAnnualCostRateCommand"/> command.
    /// </param>
    /// <returns>
    /// The <see cref="Document"/> object.
    /// </returns>
    Task<Document?> Handle(UpdateEffectiveAnnualCostRateCommand command);
    
    /// <summary>
    /// This method is used to get a document by its id.
    /// </summary>
    /// <param name="command">
    /// The <see cref="DeleteDocumentCommand"/> command.
    /// </param>
    /// <returns>
    /// The <see cref="Document"/> object.
    /// </returns>
    Task<Document?> Handle(DeleteDocumentCommand command);
}
using BillSave.API.Sales.Domain.Model.Aggregates;
using BillSave.API.Sales.Domain.Model.Commands;
using BillSave.API.Sales.Domain.Repositories;
using BillSave.API.Sales.Domain.Services;
using BillSave.API.Shared.Domain.Repositories;

namespace BillSave.API.Sales.Application.Internal.CommandServices;

/// Document command service
/// <summary>
/// The <see cref="IDocumentCommandService"/> implementation.
/// </summary>
/// <param name="documentRepository">
/// The <see cref="IDocumentRepository"/> repository.
/// </param>
/// <param name="unitOfWork">
/// The <see cref="IUnitOfWork"/> unit of work.
/// </param>
public class DocumentCommandService(IDocumentRepository documentRepository, IUnitOfWork unitOfWork)
    : IDocumentCommandService
{
    /// <inheritdoc/>
    public async Task<Document?> Handle(CreateDocumentCommand command)
    {
        var exists = await documentRepository.ExistsByCode(command.Code);
        
        if (exists)
        {
            throw new Exception("Document with the same code already exists.");
        }

        var document = new Document(command);

        try
        {
            await documentRepository.AddAsync(document);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while creating the document.", e);
        }
        
        return document;
    }
    
    /// <inheritdoc/>
    public async Task<Document?> Handle(UpdateDocumentCommand command)
    {
        var document = await documentRepository.FindByIdAsync(command.Id);
        
        if (document == null)
        {
            throw new Exception("Document not found.");
        }
        
        document.UpdateDocument(command);

        try
        {
            documentRepository.Update(document);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while updating the document.", e);
        }

        return document;
    }
}
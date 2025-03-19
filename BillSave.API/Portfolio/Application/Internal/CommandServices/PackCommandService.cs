using BillSave.API.Portfolio.Domain.Services;
using BillSave.API.Shared.Domain.Repositories;
using BillSave.API.Portfolio.Domain.Repositories;
using BillSave.API.Portfolio.Domain.Model.Commands;
using BillSave.API.Portfolio.Domain.Model.Aggregates;
using BillSave.API.Portfolio.Application.ACL.OutboundServices;
using BillSave.API.Portfolio.Application.Interfaces.CommandServices;

namespace BillSave.API.Portfolio.Application.Internal.CommandServices;

/// <summary>
/// The command service for the Pack entity.
/// </summary>
/// <param name="packRepository">
/// The <see cref="IPackRepository"/> repository.
/// </param>
/// <param name="unitOfWork">
/// The <see cref="IUnitOfWork"/> instance.
/// </param>
public class PackCommandService
    (
        IUnitOfWork unitOfWork, 
        IPackRepository packRepository, 
        ExternalSalesService externalSalesService,
        IPortfolioEacrCalculationService portfolioEacrCalculationService
    ) 
    : IPackCommandService
{
    /// <inheritdoc />
    public async Task<Pack?> Handle(CreatePackCommand command)
    {
        if (await packRepository.ExistsByNameAndUserIdAsync(command.Name, command.UserId))
            throw new Exception("Pack with the same name already exists.");
        
        var pack = new Pack(command, command.UserId);

        try
        {
            await packRepository.AddAsync(pack);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return null;
        }

        return pack;
    }
    
    /// <inheritdoc />
    public async Task<Pack?> Handle(DeletePackCommand command)
    {
        var pack = await packRepository.FindByIdAsync(command.Id);
        
        if (pack == null)
            return null;

        try
        {
            packRepository.Remove(pack);
            await externalSalesService.DeleteByPortfolioIdAsync(pack.Id);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return null;
        }
        
        return pack;
    }
    
    /// <inheritdoc />
    public async Task<Pack?> Handle(UpdatePackCommand command)
    {
        var pack = await packRepository.FindByIdAsync(command.Id);
        
        if (pack == null)
            return null;

        pack.UpdatePack(command);
        
        try
        {
            packRepository.Update(pack);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return null;
        }
        
        return pack;
    }
    
    /// <inheritdoc/>
    public async Task Handle(UpdateQuantityOfDocumentsCommand command)
    {
        var pack = await packRepository.FindByIdAsync(command.PackId);

        if (pack == null)
            throw new KeyNotFoundException("Pack not found");

        switch (command.Operation)
        {
            case "increment":
                pack.UpdateTotalDocuments(pack.TotalDocuments + 1);
                break;
            
            case "decrement":
                if (pack.TotalDocuments > 0)
                    pack.UpdateTotalDocuments(pack.TotalDocuments - 1);
                break;
            
            default:
                throw new ArgumentException("Invalid operation type");
        }
            
        packRepository.Update(pack);
        await unitOfWork.CompleteAsync(); 
    }

    /// <inheritdoc/> 
    public async Task Handle(UpdateEffectiveAnnualCostRateCommand command)
    {
        var pack = await packRepository.FindByIdAsync(command.PackId);
        
        if (pack == null)
            throw new KeyNotFoundException("Pack not found");
        
        var nominalAmounts = 
            await externalSalesService.GetDocumentNominalAmountsByPortfolioIdAsync(command.PackId);
        
        var effectiveAnnualCostRates = 
            await externalSalesService.GetDocumentEffectiveAnnualCostRatesByPortfolioIdAsync(command.PackId);
        
        var eacr = 
            await portfolioEacrCalculationService.CalculateEffectiveAnnualCostRate
                (nominalAmounts, effectiveAnnualCostRates);
        
        pack.UpdateEffectiveAnnualCostRate(eacr);
        
        packRepository.Update(pack);
    }
}
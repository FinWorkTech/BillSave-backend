using System.IO.Pipes;
using BillSave.API.Portfolio.Application.ACL.OutboundServices;
using BillSave.API.Portfolio.Application.Contracts;
using BillSave.API.Portfolio.Domain.Model.Aggregates;
using BillSave.API.Portfolio.Domain.Model.Commands;
using BillSave.API.Portfolio.Domain.Repositories;
using BillSave.API.Shared.Domain.Repositories;

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
public class PackCommandService(IPackRepository packRepository, 
    IUnitOfWork unitOfWork, ExternalSalesService externalSalesService) : IPackCommandService
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
}
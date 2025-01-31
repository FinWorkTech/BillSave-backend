using BillSave.API.Profiles.Domain.Model.Aggregates;
using BillSave.API.Profiles.Domain.Model.Commands;
using BillSave.API.Profiles.Domain.Repositories;
using BillSave.API.Profiles.Domain.Services;
using BillSave.API.Shared.Domain.Repositories;

namespace BillSave.API.Profiles.Application.Internal.CommandServices;

/// <summary>
/// Profile command service.
/// </summary>
/// <remarks>
/// This class implements the IProfileCommandService interface. It is used to handle the commands related to the Profile entity.
/// </remarks>
/// <param name="profileRepository">
/// The profile repository.
/// </param>
/// <param name="unitOfWork">
/// The unit of work. It is used to manage the transactions.
/// </param>
public class ProfileCommandService(IProfileRepository profileRepository, IUnitOfWork unitOfWork)
    : IProfileCommandService
{
    /// <inheritdoc />
    public async Task<Profile?> Handle(CreateProfileCommand command)
    {
        var profile = new Profile(command);

        try
        {
            await profileRepository.AddAsync(profile);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return null;
        }

        return null;
    }
    
    
}
using BillSave.API.Profiles.Domain.Model.Aggregates;
using BillSave.API.Profiles.Domain.Model.Commands;

namespace BillSave.API.Profiles.Domain.Services;

/// <summary>
/// Profile command service interface.
/// </summary>
public interface IProfileCommandService
{
    /// <summary>
    /// Handle the create profile command.
    /// </summary>
    /// <param name="command">
    /// The <see cref="CreateProfileCommand"/> command
    /// </param>
    /// <returns>
    /// The <see cref="Profile"/> object width the created profile information.
    /// </returns>
    Task<Profile?> Handle(CreateProfileCommand command);
}
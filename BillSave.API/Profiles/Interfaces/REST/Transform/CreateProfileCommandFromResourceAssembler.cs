using BillSave.API.Profiles.Domain.Model.Commands;
using BillSave.API.Profiles.Interfaces.REST.Resources;

namespace BillSave.API.Profiles.Interfaces.REST.Transform;

/// <summary>
/// Create profile command from resource assembler.
/// </summary>
public static class CreateProfileCommandFromResourceAssembler
{
    /// <summary>
    /// Convert a create profile resource to a create profile command.
    /// </summary>
    /// <param name="resource">
    /// The create profile resource.
    /// </param>
    /// <returns>
    /// The create profile command.
    /// </returns>
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource resource)
    {
        return new CreateProfileCommand(resource.FullName);
    }
}
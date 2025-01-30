using BillSave.API.Profiles.Domain.Model.Commands;

namespace BillSave.API.Profiles.Domain.Model.Aggregates;

/// <summary>
/// Profile aggregate root. 
/// </summary>
/// <remarks>
/// This class represents the Profile aggregate root. It contains the properties and methods that are used to manage the Profile information.
/// </remarks>
public partial class Profile
{
    public int Id { get; }
    public string FullName { get; private set; }

    public Profile()
    {
        FullName = string.Empty;
    }

    public Profile(string fullName)
    {
        FullName = fullName;
    }

    public Profile(CreateProfileCommand command)
    {
        FullName = command.FullName;
    }
}
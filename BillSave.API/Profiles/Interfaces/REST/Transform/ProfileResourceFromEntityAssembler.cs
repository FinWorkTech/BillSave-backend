using BillSave.API.Profiles.Domain.Model.Aggregates;
using BillSave.API.Profiles.Interfaces.REST.Resources;

namespace BillSave.API.Profiles.Interfaces.REST.Transform;

public static class ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResourceFromEntity(Profile entity)
    {
        return new ProfileResource(entity.Id, entity.FullName);
    }
}
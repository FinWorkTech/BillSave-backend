using BillSave.API.IAM.Application.Interfaces.QueryServices;
using BillSave.API.IAM.Domain.Repositories;
using BillSave.API.IAM.Domain.Model.Queries;
using BillSave.API.IAM.Domain.Model.Aggregates;

namespace BillSave.API.IAM.Application.Internal.QueryServices;

/// <summary>
/// User query service. 
/// </summary>
/// <param name="userRepository">
/// The <see cref="IUserRepository"/> instance.
/// </param>
public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    // <inheritdoc/>
    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.Id);
    }

    // <inheritdoc/>
    public async Task<User?> Handle(GetUserByUsernameQuery query)
    {
        return await userRepository.FindByUsernameAsync(query.Username);
    }
}
using BillSave.API.IAM.Domain.Model.Aggregates;
using BillSave.API.IAM.Domain.Model.Queries;
using BillSave.API.IAM.Domain.Repositories;
using BillSave.API.IAM.Domain.Services;

namespace BillSave.API.IAM.Application.QueryServices;

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
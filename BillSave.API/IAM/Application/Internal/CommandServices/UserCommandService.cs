using BillSave.API.IAM.Application.ACL.InboundServices;
using BillSave.API.IAM.Application.ACL.OutboundServices;
using BillSave.API.IAM.Application.Interfaces.CommandServices;
using BillSave.API.IAM.Domain.Repositories;
using BillSave.API.IAM.Domain.Model.Commands;
using BillSave.API.Shared.Domain.Repositories;
using BillSave.API.IAM.Domain.Model.Aggregates;

namespace BillSave.API.IAM.Application.Internal.CommandServices;

/// <summary>
/// User command service 
/// </summary>
/// <param name="userRepository">
/// The <see cref="IUserRepository"/> instance
/// </param>
/// <param name="tokenService">
/// The <see cref="ITokenService"/> instance
/// </param>
/// <param name="hashingService">
/// The <see cref="IHashingService"/> instance
/// </param>
/// <param name="unitOfWork">
/// The <see cref="IUnitOfWork"/> instance
/// </param>
public class UserCommandService(IUserRepository userRepository, ITokenService tokenService, 
    IHashingService hashingService, IUnitOfWork unitOfWork, ExternalProfileService externalProfileService
) : IUserCommandService
{
    // <inheritdoc/>
    public async Task Handle(SignUpCommand command)
    {
        if (userRepository.ExistsByUsername(command.Username))
            throw new Exception($"Username {command.Username} already exists");

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, hashedPassword);
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();

            var userId = await externalProfileService.CreateProfile(command.FullName);

            if (userId == 0)
                throw new Exception("Error creating profile");
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while creating the user: {e.Message}");
        }
    }

    // <inheritdoc/>
    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username);
        
        if (user is null) throw new Exception($"User {command.Username} not found");
        
        if (!hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid password");
        
        var token = tokenService.GenerateToken(user);
        
        return (user, token);
    }
}
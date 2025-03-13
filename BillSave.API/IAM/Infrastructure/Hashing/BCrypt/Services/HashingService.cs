using BillSave.API.IAM.Application.ACL.InboundServices;
using BCryptNet = BCrypt.Net.BCrypt;

namespace BillSave.API.IAM.Infrastructure.Hashing.BCrypt.Services;

/// <summary>
/// Hashing service using BCrypt algorithm. 
/// </summary>
public class HashingService : IHashingService
{
    // <inheritdoc />
    public string HashPassword(string password)
    {
        return BCryptNet.HashPassword(password);
    }

    // <inheritdoc />
    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCryptNet.Verify(password, passwordHash);
    }
}
using BillSave.API.IAM.Application.ACL.InboundServices;
using BillSave.API.IAM.Domain.Model.Queries;
using BillSave.API.IAM.Domain.Services;
using BillSave.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace BillSave.API.IAM.Infrastructure.Pipeline.Middleware.Components;

public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IUserQueryService userQueryService, ITokenService tokenService)
    {
        Console.Write("Entering InvokeAsync");
        
        // Check if the endpoint has AllowAnonymousAttribute. If it does, skip authorization
        var allowAnonymous = context.Request.HttpContext.GetEndpoint()!.Metadata
            .Any(m => m is AllowAnonymousAttribute);
        
        Console.Write($"AllowAnonymous: {allowAnonymous}");
        
        if (allowAnonymous)
        {
            await next(context);
            return;
        }

        // Check if the endpoint has AuthorizeAttribute. If it does, authorize the request
        Console.Write("Entering Authorization");
        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        
        // If token is not found, return 401
        if (token is null) throw new UnauthorizedAccessException("Token not found");
        var userId = await tokenService.ValidateToken(token);
        
        // If token is invalid, return 401
        if (userId is null) throw new UnauthorizedAccessException("Invalid token");
        var getUserByIdQuery = new GetUserByIdQuery(userId.Value);
        var user = await userQueryService.Handle(getUserByIdQuery);
        
        // If user is not found, return 401
        if (user is null) throw new UnauthorizedAccessException("User not found");
        context.Items["User"] = user;
        await next(context);
    }
}
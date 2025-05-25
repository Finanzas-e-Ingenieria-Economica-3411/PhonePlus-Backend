
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.JsonWebTokens;
using PhonePlus.Common.Auth;
using PhonePlus.Domain.Repositories;

namespace PhonePlus.Interface.Middleware;

public class AuthorizationMiddleware(RequestDelegate next) 
{
    
    public async Task InvokeAsync(HttpContext context, 
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        ITokenService tokenService)
    {
        Console.WriteLine("Entering Authorization Middleware");
        var endpoint = context.GetEndpoint();
        if (endpoint == null)
        {
            await next(context);
            return;
        }
        
        if (context.Request.Path.StartsWithSegments("/api/v1/auth/verify-email"))
        {
            Console.WriteLine("Skipping Authorization Middleware for verify-email endpoint");
            await next(context);
            return;
        }
        
        var allowAnonymous = endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>();
        if (allowAnonymous == null)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
            var userId = await tokenService.ValidateToken(token);
            if (userId == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
            var user = await userRepository.FindByIdAsync(userId.Value);
            if (user == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
            var role = await roleRepository.GetRoleName(user.RoleId);
            if (role == null)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Bearer");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            context.User = claimsPrincipal;
        }
        await next(context);
        

    }
}
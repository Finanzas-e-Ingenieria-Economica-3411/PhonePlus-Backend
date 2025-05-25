using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PhonePlus.Interface.Filter;

public class RoleFilterAuthorize : IAuthorizationFilter
{
    private readonly string[] _requiredRoles;

    public RoleFilterAuthorize(params string[] requiredRoles)
    {
        _requiredRoles = requiredRoles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        
        if (!user.Identity?.IsAuthenticated ?? true)
        {
            context.Result = new ForbidResult();
            return;
        }
        
        var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        Console.WriteLine($"RoleClaim: {roleClaim?.Value}, RequiredRoles: {string.Join(", ", _requiredRoles)}");

        if (roleClaim == null || !_requiredRoles.Contains(roleClaim.Value))
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}
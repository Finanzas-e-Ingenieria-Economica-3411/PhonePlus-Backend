using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PhonePlus.Interface.Filter;

public class RoleAuthorizeAttribute : TypeFilterAttribute
{
    public RoleAuthorizeAttribute(params string[] roles) 
        : base(typeof(RoleFilterAuthorize))
    {
        Arguments = [roles];
    }
}
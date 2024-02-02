using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CustomPermissionValidationAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] Roles;

        public CustomPermissionValidationAttribute(params string[] roles)
        {
            Roles = roles;
        }

    public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if the user is authenticated
            if (!context.HttpContext.User.Identity!.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!Roles.Any(role => context.HttpContext.User.IsInRole(role)))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}

using IJobs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Utilities
{
    public class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private ICollection<Role> _roles;
        public AuthorizationAttribute(params Role[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var unauthorizedStatusCodeObject = new Microsoft.AspNetCore.Mvc.JsonResult(new { Message = "Unauthorized!" }) { StatusCode = StatusCodes.Status401Unauthorized };
            if (_roles == null)
            {
                context.Result = unauthorizedStatusCodeObject;
            }
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<IAllowAnonymous>().Any();
            if (allowAnonymous)
                return;

            var user = (User)context.HttpContext.Items["User"];
            if( user == null || ! _roles.Contains(user.Role))
            {
                context.Result = unauthorizedStatusCodeObject;
            }

        }
    }
}

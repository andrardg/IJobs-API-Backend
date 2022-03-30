using IJobs.Utilities.JWTUtils;
using IJobs.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IJobs.Models.Base;

namespace IJobs.Utilities
{
    public class JWTMiddleware<TEntity> where TEntity : BaseEntity
    {
        private readonly RequestDelegate _next;
        public JWTMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext, IUserService userService, IJWTUtils<TEntity> ijwtUtils)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split("").Last();
            var userId = ijwtUtils.ValidateJWTToken(token);

            if(userId != Guid.Empty)
            {
                var user = userService.GetById(userId);
                if( user.Role.Equals("User"))
                    httpContext.Items["User"] = user;
                else if (user.Role.Equals("Admin"))
                    httpContext.Items["Admin"] = user;
            }
            await _next(httpContext);
        }

        //public async Task Invoke(HttpContext httpContext, ICompanyService companyService, IJWTUtils<TEntity> ijwtUtils)
        //{
        //    var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split("").Last();
        //    var companyId = ijwtUtils.ValidateJWTToken(token);

        //    if (companyId != Guid.Empty)
        //    {
        //        httpContext.Items["Company"] = companyService.GetById(companyId);
        //    }
        //    await _next(httpContext);
        //}
    }
}

using BusinessSolutionsLayer.Models;
using BusinessSolutionsLayer.Services;
using BusinessSolutionsLayer.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace TODO
{
    public class AuthFilter : Attribute, IAuthorizationFilter
    {
        private readonly RoleId role;

        public AuthFilter(RoleId role = RoleId.User)
        {
            this.role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.Filters.Any(x => x is IAllowAnonymousFilter))
            {
                return;
            }

            if (Guid.TryParse(context.HttpContext.Request.Cookies[CookiesKeys.ID], out var userId) &&
                Guid.TryParse(context.HttpContext.Request.Cookies[CookiesKeys.Signature], out var sessionId))
            {
                var sessionService = context.HttpContext.RequestServices.GetService(typeof(ISessionService)) as ISessionService;

                var session = sessionService.Get(sessionId);

                if (session == null || !session.User.Id.Equals(userId))
                {
                    context.Result = new ForbidResult();
                }
                else if (session.User.Role > this.role)
                {
                    context.Result = new UnauthorizedResult();
                }

                var userService = context.HttpContext.RequestServices.GetService(typeof(IUserService)) as IUserService;
                userService.Authorize(userId);
            }
            else
            {
                context.Result = new ForbidResult();
            }
        }
    }
}

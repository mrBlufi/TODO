using BusinessSolutionsLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace TODO
{
    public class AuthFilter : Attribute, IAuthorizationFilter
    {
        private readonly IUsersService userService;

        public AuthFilter(IUsersService userService)
        {
            this.userService = userService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(context.Filters.Any(x => x is IAllowAnonymousFilter))
            {
                return;
            }

            if (String.IsNullOrEmpty(context.HttpContext.Request.Cookies["UserName"]) ||
               !userService.GetSignature(context.HttpContext.Request.Cookies["UserName"])
                .Equals(context.HttpContext.Request.Cookies["UserSignature"]))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}

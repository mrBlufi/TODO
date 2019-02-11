using BusinessSolutionsLayer.Models;
using BusinessSolutionsLayer.Services;
using BusinessSolutionsLayer.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TODO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService usersService;
        private readonly ISessionService sessionService;

        public UserController(IUserService usersService, ISessionService sessionService)
        {
            this.usersService = usersService;
            this.sessionService = sessionService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult LogIn([FromBody]User user)
        {
            if (usersService.IdentUser(user))
            {
                return Authorize(user);
            }
            return new ForbidResult();
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public IActionResult SignUp([FromBody]User user)
        {
            usersService.AddUser(user);
            return Authorize(usersService.Get(user.Login));
        }

        private IActionResult Authorize(User user)
        {
            var session = sessionService.Create(user, DateTime.Now.AddMinutes(25));

            var option = new CookieOptions()
            {
                Expires = session.ExperationTime
            };

            HttpContext.Response.Cookies.Append(CookiesKeys.ID, session.User.Id.ToString(), option);
            HttpContext.Response.Cookies.Append(CookiesKeys.Signature, session.Id.ToString(), option);

            return LocalRedirect("~/api/task");
        }
    }
}
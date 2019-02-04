using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessSolutionsLayer.Models;
using BusinessSolutionsLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TODO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUsersService usersService;

        public UserController(IUsersService usersService) => this.usersService = usersService;

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult LogIn([FromBody]User user)
        {
            if (usersService.IdentUser(user))
            {
                return Authorize(user);
            }
            return Ok("Auth Failed");
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public IActionResult SignUp([FromBody]User user)
        {
            user = usersService.AddUser(user);
            return Authorize(user);
        }

        private IActionResult Authorize(User user)
        {
            var option = new CookieOptions()
            {
                Expires = DateTime.Now.AddMinutes(25)
            };
            HttpContext.Response.Cookies.Append("UserName", user.Login, option);
            HttpContext.Response.Cookies.Append("UserSignature", usersService.GetSignature(user.Login), option);
            return Ok();
        }
    }
}
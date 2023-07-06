using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiApp.Data;
using WebApiApp.Models;
using WebApiApp.Services;

namespace WebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly UserServices _userServices;
        public UserController(UserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("login")]
        public ActionResult<User> Login(User request)
        {
            var user = _userServices.Login(request);

            if (user == null)
            {
                return BadRequest("Username or password is incorrect");
            }
            var token = _userServices.CreateToken(user);
            var respone = new
            {
                User = user,
                Token = token
            };
            return Ok(respone);
        }
    }
}



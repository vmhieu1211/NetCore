using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiApp.Data;
using WebApiApp.Models;

namespace WebApiApp.Services
{
    public class UserServices
    {
        private readonly IConfiguration _configuration;
        public readonly DataContext _user;
        public UserServices(DataContext user, IConfiguration configuration)
        {
            _user = user;
            _configuration = configuration;
        }

        public  User  Login(User request)
        {
            var user = _user.User.FirstOrDefault(u => u.Username == request.Username);

            if (user == null)
            {
                return null;
            }

            if (user.Password != request.Password)
            {
                return null;
            }

            var token = CreateToken(user);
            var respone = new
            {
                User = user,
                Token = token
            };
            return user;
        }
        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}

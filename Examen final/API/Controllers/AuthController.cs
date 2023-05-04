using API.Auth;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ExamenfinalContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ExamenfinalContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [Route("login")]
        [HttpPost]
        public async Task<UserToken?> Login(UserAuth userCreds)
        {
            var user = await _context.Usuarios
                            .Where(u => u.Username == userCreds.User)
                            .FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            if (user.Password == userCreds.Password)
            {
                return new UserToken
                {
                    Id = user.Id,
                    Username = user.Username,
                    Token = CustomTokenJWT(user.Username)
                };
            }
            return null;
        }
        private string CustomTokenJWT(string username)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!)
            );
            var _signingCredentials = new SigningCredentials(
                _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
            );
            var _Header = new JwtHeader(_signingCredentials);
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, username)
            };
            var _Payload = new JwtPayload(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: _Claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(2)
            );
            var _Token = new JwtSecurityToken(_Header, _Payload);
            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Store.Entity.ViewModels;
using Store.Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _LoginRepository;
        private readonly IConfiguration _configuration;

        public LoginController(ILoginRepository LoginRepository, IConfiguration Configuration)
        {
            _LoginRepository = LoginRepository;
            _configuration = Configuration;
        }

        [HttpPost("")]
        public IActionResult Token(LoginModel UserData)
        {
            if (UserData != null && UserData.EmailId != null && UserData.Password != null)
            {
                var user = _LoginRepository.GetUser(UserData.EmailId, UserData.Password);
                if (user != null && user.Status == "Pending")
                {
                    return Ok("Verify your Email.");
                }

                if (user != null && BCrypt.Net.BCrypt.Verify(UserData.Password, user.Password))
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]!),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("role", user.Roles!)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("validate")]
        public async Task<string> ValidateEmail(int UserId, string token)
        {
            return await Task.FromResult(_LoginRepository.ValidateEmail(UserId, token));
        }
    }
}

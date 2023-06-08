using Microsoft.AspNetCore.Authorization;
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
    public class LoginController : Controller
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
                    if (UserData.LastLogin != null)
                    {
                        _LoginRepository.UpdateLoginDetails(user.UserId, UserData.LastLogin);
                    }
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]!),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("role", user.Roles.ToString()),
                        new Claim("FirstName", user.FirstName!),
                        new Claim("LastName", user.LastName!),
                        new Claim("Email", user.Email!),
                        //new Claim("LastLogin", user.LastLogin!)
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
        public IActionResult ValidateEmail(int UserId, string token)
        {
            var message = _LoginRepository.ValidateEmail(UserId, token);
            var User = _LoginRepository.GetUserById(UserId);
            return Json(new { message, User });
        }

        [HttpPost("resetpassword")]
        [Authorize()]
        public IActionResult ResetPassword(string email, string password, string newPassword)
        {
            var user = _LoginRepository.GetUser(email, password);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                var status = _LoginRepository.ResetPassword(user.UserId, newPassword);
                return Json(new { status });
            }
            return Json(new { status = false });
        }
    }
}

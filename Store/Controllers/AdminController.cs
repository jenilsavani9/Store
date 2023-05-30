using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Entity.ViewModels;
using Store.Repository.Interface;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _AdminRepository;
        private readonly IConfiguration _configuration;

        public AdminController(IAdminRepository AdminRepository, IConfiguration Configuration)
        {
            _AdminRepository = AdminRepository;
            _configuration = Configuration;
        }

        [HttpPost("")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<AddUserModel>> AddUser(AddUserModel user)
        {
            var TempUser = _AdminRepository.GetUserByEmail(user.Email);
            if (TempUser != null)
            {
                return BadRequest("Email Already Registered.");
            }
            var NewUser = _AdminRepository.AddUser(user);
            _AdminRepository.SendMail(NewUser);
            return await Task.FromResult(user);
        }
    }
}

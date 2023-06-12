using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Entity.Models;
using Store.Entity.ViewModels;
using Store.Repository.Interface;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _AdminRepository;
        private readonly IConfiguration _configuration;

        public AdminController(IAdminRepository AdminRepository, IConfiguration Configuration)
        {
            _AdminRepository = AdminRepository;
            _configuration = Configuration;
        }

        [HttpPost("User/Add")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<AddUserModel>> AddUser(AddUserModel user)
        {
            var TempUser = _AdminRepository.GetUserByEmail(user.Email);
            if (TempUser != null)
            {
                return BadRequest("Email Already Registered.");
            }
            var NewUser = _AdminRepository.AddUser(user);
            _AdminRepository.SendMail(user);
            return await Task.FromResult(user);
        }

        [HttpPost("User/Get")]
        [Authorize(Roles = "admin")]
        public IActionResult GetUsers(int pageIndex, string search)
        {
            var result = _AdminRepository.GetUsersList(pageIndex, search);
            var userCount = _AdminRepository.GetUserCount();
            return Json(new { result, userCount });
        }

        [HttpPost("User/Delete")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteUsers(int userId)
        {
            var result = _AdminRepository.DeleteUser(userId);
            return Json(new { result });
        }
    }
}

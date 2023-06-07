using Store.Entity.Data;
using Store.Entity.Models;
using Store.Entity.ViewModels;
using Store.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly StoreContext _db;

        public AdminRepository(StoreContext db)
        {
            _db = db;
        }

        public User GetUserByEmail(string Email)
        {
            var result = _db.Users.FirstOrDefault(u => u.Email == Email);
            return result;
        }

        public User AddUser(AddUserModel user)
        {
            try
            {
                User TempUser = new User();
                TempUser.FirstName = user.FirstName;
                TempUser.LastName = user.LastName;
                TempUser.Email = user.Email;
                TempUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                TempUser.Status = "pending";
                TempUser.Roles = 2;
                _db.Users.Add(TempUser);
                _db.SaveChanges();

                return TempUser;
            }
            catch
            {
                throw;
            }
        }

        public bool SendMail(AddUserModel user)
        {
            var token = Guid.NewGuid().ToString();

            // Store the token in the email token table with the user's email
            var TempUser = _db.Users.FirstOrDefault(u => u.Email == user.Email);
            if(TempUser == null)
            {
                return false;
            }
            var EmailValid = new MailToken
            {
                UserId = TempUser.UserId,
                Token = token,
                CreatedAt = DateTime.Now,
            };

            _db.MailTokens.Add(EmailValid);
            _db.SaveChanges();

            var resetLink = "http://localhost:3000/verify?UserId=" + TempUser.UserId + "&token=" + token;

            var fromAddress = new MailAddress("jenilsavani8@gmail.com", "Store Inc.");
            var toAddress = new MailAddress(user.Email);
            var subject = "Store Email Verify";
            var body = $"Hi,<br /><br />Please click on the following link to Validate Mail ID:<br /><br />" +
                $"<a href='{resetLink}'>{resetLink}</a><br /> Please, Use Below Credentionals to login into your account.<br />" +
                $"Email: {user.Email}<br/>Password : {user.Password}<br/> Thank You!!!";
            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("jenilsavani8@gmail.com", "bwgnmdxyggqrylsu"),
                EnableSsl = true
            };
            smtpClient.Send(message);

            return true;
        }

        // users list
        public object GetUsersList(int pageIndex, string search)
        {
            var users = from r in _db.Users.AsEnumerable() select new
            {
                r.UserId,
                r.FirstName,
                r.LastName,
                r.Email,
                r.Status,
                r.Roles,
            };
            var UserCount = users.Count();
            return users.Skip(pageIndex * 10).Take(10).ToList();
        }

        public int GetUserCount()
        {
            var userCount = _db.Users.Count();
            return userCount;
        }

        public string DeleteUser(int UserId)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserId == UserId);
            if(user == null)
            {
                return "User Not Found";
            }
            if(user.Status == "deactive")
            {
                return "User Already Deleted";
            }
            user.Status = "deactive";
            user.UpdatedAt = DateTime.Now;
            _db.SaveChanges();
            return "User Deleted Successfully";
        }
    }
}

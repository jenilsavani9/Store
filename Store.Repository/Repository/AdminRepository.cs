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
                TempUser.Roles = "customer";
                _db.Users.Add(TempUser);
                _db.SaveChanges();

                return TempUser;
            }
            catch
            {
                throw;
            }
        }

        public bool SendMail(User user)
        {
            var token = Guid.NewGuid().ToString();

            // Store the token in the email token table with the user's email
            var EmailValid = new MailToken
            {
                UserId = user.UserId,
                Token = token,
                CreatedAt = DateTime.Now,
            };

            _db.MailTokens.Add(EmailValid);
            _db.SaveChanges();

            var resetLink = "https://localhost:44372/api/Login/validate?UserId=" + user.UserId + "&token=" + token;

            var fromAddress = new MailAddress("jenilsavani8@gmail.com", "ASP");
            var toAddress = new MailAddress(user.Email);
            var subject = "Store Email Verify";
            var body = $"Hi,<br /><br />Please click on the following link to Validate Mail ID:<br /><br /><a href='{resetLink}'>{resetLink}</a>";
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
    }
}

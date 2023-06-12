using Store.Entity.Data;
using Store.Entity.Models;
using Store.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly StoreContext _db;

        public LoginRepository(StoreContext db)
        {
            _db = db;
        }

        public User GetUser(string Email, string Password)
        {
            return _db.Users.FirstOrDefault(u => u.Email == Email && u.Status == Status.active)!;
        }

        public bool ValidateEmail(int UserId, string token)
        {
            var Entry = _db.Tokens.FirstOrDefault(e => e.UserId == UserId && e.UpdatedAt == null);
            if (Entry == null || Entry!.MailToken != token)
            {
                return false;
            }
            if(Entry.CreatedAt.AddHours(24) <= DateTime.Now)
            {
                Entry.UpdatedAt = DateTime.Now;
                _db.SaveChanges();
                return false;
            }
            var user = _db.Users.FirstOrDefault(u => u.UserId == Entry.UserId && u.Status == Status.pending);
            if(user == null)
            {
                return false;
            }
            user.Status = Status.active;
            Entry.UpdatedAt = DateTime.Now;
            _db.SaveChanges();
            return true;

        }

        public object? GetUserById(int UserId)
        {
            var User = from u in _db.Users.AsEnumerable()
                        where u.UserId == UserId
                        select new
                        {
                            u.UserId,
                            u.FirstName,
                            u.LastName,
                            u.Email,
                            u.LastLogin
                        };
            if(User == null)
            {
                return null;
            }
            return User;
        }

        public bool ResetPassword(long UserId, string newPassword)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserId == UserId);
            if(null == user || newPassword.Length < 5)
            {
                return false;
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.UpdatedAt = DateTime.Now;
            _db.SaveChanges();
            return true;
        }

        public void UpdateLoginDetails(long UserId, string LoginDetails)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserId == UserId);
            if (null != user)
            {
                user.LastLogin = LoginDetails;
                _db.SaveChanges();
            }
        }
    }
}

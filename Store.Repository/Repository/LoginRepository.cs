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
            return _db.Users.FirstOrDefault(u => u.Email == Email && u.DeletedAt == null)!;
        }

        public string ValidateEmail(int UserId, string token)
        {
            var Entry = _db.MailTokens.FirstOrDefault(e => e.UserId == UserId && e.DeletedAt == null);
            if (Entry == null || Entry!.Token != token)
            {
                return "Link Is not valid";
            }
            var user = _db.Users.FirstOrDefault(u => u.UserId == Entry.UserId);
            if(user == null)
            {
                return "Link Is not valid";
            }
            user.Status = "active";
            Entry.DeletedAt = DateTime.Now;
            _db.SaveChanges();
            return "Email Verified.";

        }
    }
}

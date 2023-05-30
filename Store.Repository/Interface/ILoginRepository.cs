using Store.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Interface
{
    public interface ILoginRepository
    {
        public User GetUser(string EmailId, string Password);

        public string ValidateEmail(int UserId, string token);
    }
}

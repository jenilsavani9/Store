using Store.Entity.Models;
using Store.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Interface
{
    public interface IAdminRepository
    {
        public User GetUserByEmail(string Email);

        public User AddUser(AddUserModel user);

        public bool SendMail(AddUserModel user);

        public object GetUsersList(int pageIndex);

        public int GetUserCount();

        public string DeleteUser(int UserId);
    }
}

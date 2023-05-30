using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Entity.ViewModels
{
    public class AddUserModel
    {
        [StringLength(50, MinimumLength = 5, ErrorMessage = "FirstName cannot be longer than 50 characters and less than 5 characters")]
        public string FirstName { get; set; } = null!;

        [StringLength(50, MinimumLength = 5, ErrorMessage = "LastName cannot be longer than 50 characters and less than 5 characters")]
        public string LastName { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Not a valid email")]
        public string Email { get; set; } = null!;

        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password cannot be longer than 20 characters and less than 5 characters")]
        public string Password { get; set; } = null!;
    }
}

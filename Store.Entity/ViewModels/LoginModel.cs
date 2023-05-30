using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Entity.ViewModels
{
    public class LoginModel
    {
        [Required()]
        [EmailAddress(ErrorMessage = "Not a valid email")]
        public string EmailId { get; set; } = null!;

        [Required()]
        public string Password { get; set; } = null!;
    }
}

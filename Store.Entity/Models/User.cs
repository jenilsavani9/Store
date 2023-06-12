using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Entity.Models
{
    public enum Status
    {
        active, deactive, pending
    }

    public enum Roles
    {
        admin, customer
    }

    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 5, ErrorMessage = "The property First Name should have 20 maximum characters and 5 minimum characters")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 5, ErrorMessage = "The property Last Name should have 20 maximum characters and 5 minimum characters")]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(maximumLength: 50)]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public Status Status { get; set; }

        public Roles Roles { get; set; }

        public string? LastLogin { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }
}

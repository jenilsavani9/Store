using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Store.Entity.Models
{
    public class Address: Generic
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        [StringLength(maximumLength: 255)]
        public string AddressLine1 { get; set; } = null!;

        [StringLength(maximumLength: 255)]
        public string? AddressLine2 { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

    }
}

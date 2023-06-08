using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Entity.Models
{
    public class Features
    {
        [Key]
        public long FeaturesId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public string? FeaturesName { get; set;}

        public string? FeaturesDescription { get; set;}

        [Required]
        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}

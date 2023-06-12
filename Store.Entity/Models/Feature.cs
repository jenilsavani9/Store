using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Store.Entity.Models
{
    public class Feature
    {
        [Key]
        public int FeatureId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [StringLength(maximumLength: 50)]
        public string FeatureName { get; set; } = null!;

        [StringLength(maximumLength: 255)]
        public string? FeatureDescription { get; set; }

        public bool Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }
}

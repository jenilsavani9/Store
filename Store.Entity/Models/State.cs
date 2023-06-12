using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Store.Entity.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string StateName { get; set; } = null!;
    }
}

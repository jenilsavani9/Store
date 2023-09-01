using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Store.Entity.Models
{
    public class City 
    {
        [Key]
        public int CityId { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        [ForeignKey("State")]
        public int StateId { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string CityName { get; set; } = null!;
    }
}

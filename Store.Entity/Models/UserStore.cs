using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Entity.Models
{
    public class UserStore: Generic
    {
        [Key]
        public int StoreId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 5, 
            ErrorMessage = "The property Store Name should have 50 maximum characters and 5 minimum characters")]
        public string StoreName { get; set; } = null!;

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        [ForeignKey("State")]
        public int? StateId { get; set; }

        [ForeignKey("City")]
        public int? CityId { get; set; }

        [ForeignKey("Address")]
        public int? AddressId { get; set; }

        [StringLength(maximumLength: 15, MinimumLength = 3, 
            ErrorMessage = "The property Store Name should have 15 maximum characters and 3 minimum characters")]
        public string? PostalCode { get; set; }

        public string? LocationLink { get; set; }

        public bool Status { get; set; }


        public virtual City City { get; set; } = null!;

        public virtual Country Country { get; set; } = null!;

        public virtual State State { get; set; } = null!;

        public virtual User User { get; set; } = null!;

        public virtual Address Address { get; set; } = null!;

    }
}

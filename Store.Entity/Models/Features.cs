using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Entity.Models
{
    public class Features
    {
        public long FeaturesId { get; set; }

        public string? FeaturesName { get; set;}

        public string? FeaturesDescription { get; set;}

        public string? Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}

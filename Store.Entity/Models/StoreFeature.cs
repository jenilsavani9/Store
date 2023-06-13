using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Entity.Models
{
    public class StoreFeature
    {
        [Key]
        public int StoreFeatureId { get; set; }

        [ForeignKey("Feature")]
        public int FeatureId { get; set; }

        [ForeignKey("UserStore")]
        public int StoreId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public bool Status { get; set; }

        //public virtual UserStore UserStores { get; set; } = null!;

        public virtual Feature Features { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Entity.ViewModels
{
    public class StoreFeatureModal
    {
        public List<int> FeatureIds { get; set; } = null!;

        public int StoreId { get; set; }

        public int UserId { get; set; }
    }
}

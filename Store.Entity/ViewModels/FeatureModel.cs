using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Entity.ViewModels
{
    public class FeatureModel
    {
        public int FeaturesId { get; set; }

        public int UserId { get; set; }

        public string? FeaturesName { get; set; }

        public string? FeaturesDescription { get; set; }

    }
}

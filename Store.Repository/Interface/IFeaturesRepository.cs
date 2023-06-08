using Store.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Interface
{
    public interface IFeaturesRepository
    {
        public object GetFeatures(int UserId);

        public object GetFeaturesById(int FeatureId);

        public object AddFeatures(FeatureModel obj);

        public object EditFeatures(FeatureModel obj);

        public object DeleteFeatures(int FetureId);
    }
}

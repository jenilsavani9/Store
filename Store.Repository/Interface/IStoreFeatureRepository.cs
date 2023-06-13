using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Interface
{
    public interface IStoreFeatureRepository
    {
        public object GetStoreFeatures(int StoreId);

        public object GetFeatureByUserId(int UserId);

        public object AddStoreFeatures(List<int> featureIds, int StoreId, int UserId);
    }
}

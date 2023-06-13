using Store.Entity.Data;
using Store.Entity.Models;
using Store.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repository
{
    public class StoreFeatureRepository : IStoreFeatureRepository
    {
        private readonly StoreContext _db;

        public StoreFeatureRepository(StoreContext db)
        {
            _db = db;
        }

        public object GetStoreFeatures(int StoreId)
        {
            var data = from fs in _db.StoreFeatures
                       where fs.StoreId == StoreId && fs.Status == true
                       select new
                       {
                           fs.FeatureId,
                           fs.Features.FeatureName
                       };
            return data;
        }

        public object GetFeatureByUserId(int UserId)
        {
            var data = from f in _db.Features
                       where f.UserId == UserId && f.Status == true
                       select new
                       {
                           f.FeatureId,
                           f.FeatureName
                       };
            return data;
        }

        public object AddStoreFeatures(List<int> featureIds, int StoreId, int UserId)
        {
            var deleteFeatures = _db.StoreFeatures.Where(item => item.StoreId == StoreId).ToList();
            _db.StoreFeatures.RemoveRange(deleteFeatures);
            
            foreach (var id in featureIds)
            {
                _db.StoreFeatures.Add(new StoreFeature
                {
                    FeatureId = id,
                    StoreId = StoreId,
                    UserId = UserId,
                    Status = true
                });
            }
            _db.SaveChanges();
            return GetStoreFeatures(StoreId);
        }
    }
}

using Store.Entity.Data;
using Store.Entity.Models;
using Store.Entity.ViewModels;
using Store.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repository
{
    public class FeaturesRepository : IFeaturesRepository
    {
        private readonly StoreContext _db;

        public FeaturesRepository(StoreContext db)
        {
            _db = db;
        }

        public object GetFeatures(int UserId)
        {
            var result = from f in _db.Features
                         where f.UserId == UserId && f.Status == true
                         select new
                         {
                             f.UserId,
                             f.FeaturesName,
                             f.FeaturesId,
                             f.FeaturesDescription,
                             f.Status,
                         };
            return result;
        }

        public object GetFeaturesById(int FeatureId)
        {
            var result = from f in _db.Features
                         where f.FeaturesId == FeatureId && f.Status == true
                         select new
                         {
                             f.UserId,
                             f.FeaturesName,
                             f.FeaturesId,
                             f.FeaturesDescription,
                             f.Status,
                         };
            return result;
        }

        public object AddFeatures(FeatureModel obj)
        {
            Features feature = new();
            feature.UserId = obj.UserId;
            feature.FeaturesName = obj.FeaturesName;
            feature.FeaturesDescription = obj.FeaturesDescription;
            feature.Status = true;
            feature.CreatedAt = DateTime.Now;
            _db.Features.Add(feature);
            _db.SaveChanges();
            var result = GetFeaturesById((int)feature.FeaturesId);
            return result;
        }

        public object EditFeatures(FeatureModel obj)
        {
            var result = _db.Features.FirstOrDefault(f => f.FeaturesId == obj.FeaturesId && f.Status == true);
            if (result == null)
            {
                return null!;
            }
            result.FeaturesName = obj.FeaturesName;
            result.FeaturesDescription = obj.FeaturesDescription;
            result.UpdatedAt = DateTime.Now;
            _db.SaveChanges();
            var Tempresult = GetFeaturesById((int)result.FeaturesId);
            return Tempresult;
        }

        public object DeleteFeatures(int FetureId)
        {
            var result = _db.Features.FirstOrDefault(f => f.FeaturesId == FetureId && f.Status == true);
            if(result == null)
            {
                return null!;
            }
            result.Status = false;
            result.UpdatedAt = DateTime.Now;
            _db.SaveChanges();
            return result;
        }

    }
}

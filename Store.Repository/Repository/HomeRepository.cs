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
    public class HomeRepository : IHomeRepository
    {
        public readonly StoreContext _db;

        public HomeRepository(StoreContext db)
        {
            _db = db;
        }

        public object GetStores(int UserId)
        {
            var result = from r in _db.UserStores 
                         where r.UserId == UserId && r.Status == "active"
                         select new
                         {
                             r.StoreId, 
                             r.UserId,
                             r.StoreName,
                             r.Address,
                             r.Country.CountryName,
                             r.City.CityName,
                             r.State.StateName,
                             r.PostalCode,
                             r.LocationLink,
                             r.Status
                         };
            return result;
        }

        public object GetStoresById(int StoreId)
        {
            var result = from r in _db.UserStores
                         where r.StoreId == StoreId
                         select new
                         {
                             r.StoreId,
                             r.UserId,
                             r.StoreName,
                             r.Address,
                             r.Country.CountryName,
                             r.City.CityName,
                             r.State.StateName,
                             r.PostalCode,
                             r.LocationLink,
                             r.Status
                         };
            return result;
        }

        public object AddStores(StoresModel obj)
        {
            UserStore store = new()
            {
                UserId = obj.UserId,
                StoreName = obj.StoreName,
                Address = obj.Address,
                CountryId = obj.CountryId,
                StateId = obj.StateId,
                CityId = obj.CityId,
                PostalCode = obj.PostalCode,
                LocationLink = obj.LocationLink,
                Status = "active"
            };
            _db.UserStores.Add(store);
            _db.SaveChanges();
            var result = GetStoresById((int)store.StoreId);
            return result;
        }

        public object EditStores(StoresModel obj)
        {
            var store = _db.UserStores.FirstOrDefault(item=>item.StoreId == obj.StoreId);
            if(store == null)
            {
                return null!;
            }
            store.StoreName = obj.StoreName;
            store.Address = obj.Address;
            store.CityId = obj.CityId;
            store.StateId = obj.StateId;
            store.CountryId = obj.CountryId;
            store.PostalCode = obj.PostalCode;
            store.LocationLink = obj.LocationLink;
            _db.SaveChanges();
            var result = GetStoresById((int)store.StoreId);
            return result;
        }

        public object DeleteStores(int StoreId)
        {
            var result = _db.UserStores.FirstOrDefault(item => item.StoreId == StoreId);
            if (result == null)
            {
                return null!;
            }
            else
            {
                result.Status = "deactive";
                result.UpdatedAt = DateTime.Now;
                _db.SaveChanges();
                return result;
            }
        }

        public object GetCities()
        {
            var cities = from c in _db.Cities
                         select new
                         {
                             c.CityId,
                             c.CityName,
                             c.StateId,
                             c.CountryId
                         };
            return cities;
        }

        public object GetStates()
        {
            var states = from c in _db.States
                         select new
                         {
                             c.StateId,
                             c.StateName, 
                             c.CountryId
                         };
            return states;
        }

        public object GetCountry()
        {
            var countrys = from c in _db.Countries
                         select new
                         {
                             c.CountryId,
                             c.CountryName
                         };
            return countrys;
        }
    }
}

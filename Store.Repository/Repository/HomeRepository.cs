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
                         where r.UserId == UserId && r.Status == true
                         select new
                         {
                             r.StoreId,
                             r.UserId,
                             r.StoreName,
                             r.PostalCode,
                             r.LocationLink,
                             r.Address.AddressLine1,
                             r.Address.AddressLine2,
                             r.City.CityName,
                             r.Country.CountryName,
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
                             r.PostalCode,
                             r.LocationLink,
                             r.Address.AddressLine1,
                             r.Address.AddressLine2,
                             r.City.CityName,
                             r.Country.CountryName,
                             r.Status
                         };
            return result;
        }

        public object AddStores(StoresModel obj)
        {
            Address address = new()
            {
                AddressLine1 = obj.AddressLine1,
                AddressLine2 = obj.AddressLine2
            };
            _db.Addresses.Add(address);
            _db.SaveChanges();
            UserStore store = new()
            {
                UserId = obj.UserId,
                StoreName = obj.StoreName,
                CountryId = obj.CountryId,
                StateId = obj.StateId,
                CityId = obj.CityId,
                PostalCode = obj.PostalCode,
                LocationLink = obj.LocationLink,
                AddressId = address.AddressId,
                Status = true
            };
            _db.UserStores.Add(store);
            _db.SaveChanges();

            var result = GetStoresById((int)store.StoreId);
            return result;
        }

        public object EditStores(StoresModel obj)
        {
            var store = _db.UserStores.FirstOrDefault(item => item.StoreId == obj.StoreId);
            if (store == null)
            {
                return null!;
            }
            store.StoreName = obj.StoreName;

            //store.CityId = obj.CityId;
            //store.StateId = obj.StateId;
            //store.CountryId = obj.CountryId;
            store.PostalCode = obj.PostalCode;
            store.LocationLink = obj.LocationLink;
            store.UpdatedAt = DateTime.Now;

            var address = _db.Addresses.FirstOrDefault(item => item.AddressId == store.AddressId);
            if (address != null)
            {
                if (obj.AddressLine1 != null)
                {
                    address.AddressLine1 = obj.AddressLine1;
                }
                address.AddressLine2 = obj.AddressLine2;
            }

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
                result.Status = false;
                result.UpdatedAt = DateTime.Now;
                _db.SaveChanges();
                return result;
            }
        }

        public object GetCities()
        {
            var cities = from c in _db.Cities.OrderBy(item => item.CityName)
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
            var states = from c in _db.States.OrderBy(item => item.StateName)
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
            var countrys = from c in _db.Countries.OrderBy(item => item.CountryName)
                           select new
                           {
                               c.CountryId,
                               c.CountryName
                           };
            return countrys;
        }
    }
}
